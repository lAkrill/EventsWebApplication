﻿public record UserUpdateDto(
    string FirstName,
    string LastName,
    string Email,
    DateOnly Birthday,
    string Password);
