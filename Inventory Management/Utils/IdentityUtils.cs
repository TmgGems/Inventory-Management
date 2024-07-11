﻿using Inventory_Management.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Inventory_Management.Utils
{
    public class IdentityUtils
    {
        public static void AddingClaimIdentity(LogInModel user, string ? roles, HttpContext httpcontext)
        {
            //list of claims
            var userClaims = new List<Claim>()
                {
                    //new Claim("UserName", user.Username),
                    //new Claim(ClaimTypes.Email,user.Username),

                    //new Claim(ClaimTypes.Role,"user"),
                    //new Claim(ClaimTypes.Role,"admin")

                //new Claim("Key",user.Username),
                //new Claim(ClaimTypes.Email,user.Username),
                //new Claim(ClaimTypes.Name,"Ghhyamjo Lama"),
                //new Claim(ClaimTypes.Role,"admin")
                ////new Claim(ClaimTypes.Role,roles)

                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Username),
                new Claim(ClaimTypes.Role, roles ??"user")

                 };

            //create a identity claims
            var claimsIdentity = new ClaimsIdentity(
            userClaims, CookieAuthenticationDefaults.AuthenticationScheme);


            //httcontext , current user is authentic user
            //sing in user to httpcontext
            httpcontext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity)
            );
        }
    }
}
