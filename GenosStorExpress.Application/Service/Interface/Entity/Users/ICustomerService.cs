﻿using GenosStorExpress.Application.Service.Interface.Base;
using GenosStorExpress.Domain.Entity.User;

namespace GenosStorExpress.Application.Service.Interface.Entity.Users {
    public interface ICustomerService: IStandardService<Customer> {
		string GetOrdererInfo(Customer customer);
    }
}