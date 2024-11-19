﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public List<string>? ErrorMessages { get; set; }

        public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;

        public bool IsFail => !IsSuccess;


        public HttpStatusCode Status { get; set; }

        public static ServiceResult<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult<T>
            {
                Data = data,
                Status = status
            };
        }

        public static ServiceResult<T> Fail(List<string> errorMessages, HttpStatusCode status  =HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                ErrorMessages = errorMessages,
                Status = status
            };
        }

        public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>
            {
                ErrorMessages = [errorMessage],
                Status = status
            };
        }
    }

    public class ServiceResult
    {
        public List<string>? ErrorMessages { get; set; }

        public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;

        public bool IsFail => !IsSuccess;


        public HttpStatusCode Status { get; set; }

        public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult
            {
                Status = status
            };
        }

        public static ServiceResult Fail(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessages = errorMessages,
                Status = status
            };
        }

        public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult
            {
                ErrorMessages = [errorMessage],
                Status = status
            };
        }
    }
}
