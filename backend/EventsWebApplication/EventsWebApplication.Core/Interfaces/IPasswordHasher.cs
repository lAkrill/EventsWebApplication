﻿namespace EventsWebApplication.Core.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string hash, string password);
    }
}
