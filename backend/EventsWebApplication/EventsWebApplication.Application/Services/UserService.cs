using AutoMapper;
using EventsWebApplication.Application.Dtos;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Application.Interfaces;
using EventsWebApplication.Core.Models;
using MediatR;

namespace EventsWebApplication.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, 
            IMapper mapper, 
            IPasswordHasher passwordHasher,
            ITokenService tokenService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<TokenResponseDto?> LoginAsync(UserLoginDto request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user is null)
            {
                throw new LoginException("Invalid Email or password");
            }
            if (!_passwordHasher.Verify(user.PasswordHash, request.Password))
            {
                throw new LoginException("Invalid Email or password");
            }

            return await CreateTokenResponse(user);
        }

        public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user is null)
                return null;

            return await CreateTokenResponse(user);
        }

        private async Task<TokenResponseDto> CreateTokenResponse(User? user)
        {
            return new TokenResponseDto
            {
                AccessToken = _tokenService.GenerateJwtToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateUserAsync(user);
            return refreshToken;
        }

        private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user is null || user.RefreshToken != refreshToken
                || user.RefreshTokenExpiry <= DateTime.UtcNow)
            {
                return null;
            }
            return user;
        }


        public async Task<List<UserReadDto>> GetAllUsersAsync(CancellationToken ct)
        {
            var users = await _userRepository.GetAllUsersAsync(ct);
            return _mapper.Map<List<UserReadDto>>(users);
        }

        public async Task<UserReadDto> GetUserByIdAsync(Guid id, CancellationToken ct)
        {
            var user = await _userRepository.GetUserByIdAsync(id, ct);
            if (user == null)
            {
                throw new NotFoundException($"User not found with id: {id}");
            }

            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserParticipantDto> GetUserWithEventsByIdAsync(Guid id, CancellationToken ct)
        {
            var user = await _userRepository.GetUserByIdAsync(id, ct);
            if (user == null)
            {
                throw new NotFoundException($"User not found with id: {id}");
            }

            return _mapper.Map<UserParticipantDto>(user);
        }

        public async Task<UserReadDto> CreateUserAsync(UserRegisterDto userRegisterDto,  CancellationToken ct)
        {
            if(userRegisterDto == null)
            {
                throw new ArgumentNullException(nameof(UserRegisterDto));
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(userRegisterDto.Email, ct);
            if (existingUser != null)
            {
                throw new NotUniqueException($"Category with Email {userRegisterDto.Email} already exists");
            }

            var hashPassword = _passwordHasher.Hash(userRegisterDto.Password);

            var newUser = _mapper.Map<User>(userRegisterDto);
            newUser.PasswordHash = hashPassword;
            await _userRepository.AddUserAsync(newUser, ct);
            return _mapper.Map<UserReadDto>(newUser);
        }

        public async Task<UserReadDto> UpdateUserAsync(UserUpdateDto userUpdateDto, CancellationToken ct)
        {
            if (userUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(userUpdateDto));
            }

            var existingUser = await _userRepository.GetUserByIdAsync(userUpdateDto.Id, ct);
            if (existingUser == null)
            {
                throw new NotFoundException($"User not found with id: {userUpdateDto.Id}");
            }

            var hashPassword = _passwordHasher.Hash(userUpdateDto.Password);

            var newUser = _mapper.Map(userUpdateDto, existingUser);
            newUser.PasswordHash = hashPassword;
            await _userRepository.UpdateUserAsync(newUser, ct);
            return _mapper.Map<UserReadDto>(newUser);
        }

        public async Task DeleteUserByIdAsync(Guid id, CancellationToken ct)
        {
            var deletedUser = await _userRepository.GetUserByIdAsync(id, ct);
            if (deletedUser == null)
            {
                throw new NotFoundException($"User not found with id: {id}");
            }

            await _userRepository.DeleteUserAsync(id, ct);
        }
    }
}
