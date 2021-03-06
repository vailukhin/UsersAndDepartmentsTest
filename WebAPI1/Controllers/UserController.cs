﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI1.Models;

namespace WebAPI1.Controllers
{
    public class UserController : ApiController
    {
        ApplicationDbContext _db;

        public UserController()
        {
            _db = new ApplicationDbContext();
        }

        // GET api/user
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        public List<UserDTO> GetAll()
        {
            List<UserDTO> res = new List<UserDTO>();

            foreach (var userDT in _db.Users.ToList())
            {
                var depDT = _db.Departments.FirstOrDefault(x => x.Id == userDT.DepartmentId);

                res.Add(new UserDTO()
                {
                    Id = userDT.Id,
                    UserName = userDT.UserName,
                    DepartmentId = (depDT != null)? depDT.Id: 0,
                    Department = (depDT != null) ? depDT.Title:"Не найден"
                });
            }

            return res;
        }

        // GET api/values/5
        public User Get(int id)
        {
            return new Models.User() { };
        }

        // POST api/values
        public void Post(UserCreateDto user)
        {
            var userDT = new User();
            userDT.UserName = user.UserName;
            userDT.DepartmentId = user.DepartmentId;

            _db.Users.Add(userDT);
            _db.SaveChanges();

        }

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        public void Delete(int id)
        {
            var userDT = _db.Users.FirstOrDefault(x => x.Id == id);

            if (userDT != null)
            {
                _db.Users.Remove(userDT);
                _db.SaveChanges();

            }
        }
    }
}
