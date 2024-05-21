using ShortLink.Application.DTOs.Account;
using ShortLink.Application.Interfaces;
using ShortLink.Domain.Interfaces;
using ShortLink.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLink.Application.Services
{
    public class UserService : IUserService
    {
        #region Ctor
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        public UserService(IUserRepository userRepository, IPasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;

        }

        
        #endregion

        public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO userDTO)
        {
            //check mobile is exists
            if (!await _userRepository.CheckEmailIsExist(userDTO.Mobile))
            {
                var new_user = new User()
                {
                    Mobile = userDTO.Mobile,
                    CreateDate = DateTime.Now,
                    FirstName = userDTO.FirstName,
                    IsDelete = false,
                    LastName = userDTO.LastName,
                    IsUserBlock = false,
                    Password = _passwordHelper.EncodePasswordMD5(userDTO.Password),
                    MobileActiveCode = new Random().Next(10000,9999999).ToString()
                };

                await _userRepository.AddUser(new_user);
                await _userRepository.Save();

                return RegisterUserResult.Success;
            }
            else
            {
                return RegisterUserResult.IsMobileExist;
            }
        }


        public async Task<LoginUserResult> LoginUser(LoginUserDTO userDTO)
        {
            var user_data = await _userRepository.GetUserByMobile(userDTO.Mobile);

            if (user_data == null)
            {
                return LoginUserResult.NotFound;
            }

            if (user_data.IsMobileActive == false)
            {
                return LoginUserResult.NotActive;
            }

            if (user_data.Password != _passwordHelper.EncodePasswordMD5(userDTO.Password))
            {
                return LoginUserResult.NotFound;
            }
            return LoginUserResult.Success;

        }

        public async Task<User> GetUserByMobile(string mobile)
        {
            return await _userRepository.GetUserByMobile(mobile);
        }
    }
}
