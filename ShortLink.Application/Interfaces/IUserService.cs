﻿using ShortLink.Application.DTOs.Account;
using ShortLink.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLink.Application.Interfaces
{
    public interface IUserService
    {
        Task<RegisterUserResult> RegisterUser(RegisterUserDTO userDTO);
        Task<LoginUserResult> LoginUser(LoginUserDTO userDTO);
        Task<User> GetUserByMobile(string mobile);

        
    }
}
