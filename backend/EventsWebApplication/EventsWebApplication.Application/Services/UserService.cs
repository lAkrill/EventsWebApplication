using AutoMapper;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Application.Interfaces;
using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
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

            var newUser = _mapper.Map<User>(userRegisterDto);
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

            var newUser = _mapper.Map(userUpdateDto, existingUser);
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
