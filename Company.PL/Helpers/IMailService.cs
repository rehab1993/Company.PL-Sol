﻿using Company.DAL.Models;

namespace Company.PL.Helpers
{
    public interface IMailService
    {
        public void SendEmail(Email email); 
    }
}
