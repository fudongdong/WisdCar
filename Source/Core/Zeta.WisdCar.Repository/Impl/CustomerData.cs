﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeta.WisdCar.Model.Entity;
using Zeta.WisdCar.Model.PO;
using Zeta.WisdCar.Repository.CRUD;

namespace Zeta.WisdCar.Repository.Impl
{
    public class CustomerData : ICustomerData
    {
        private Customer _daoCustomer = new Customer();
        public System.Data.DataSet GetCustomers(CustomerQueryEntity filter)
        {
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();

            if (!string.IsNullOrEmpty(filter.Name.Trim()))
            {
                strSql1.AppendFormat(" Name like '%{0}%' ", filter.Name);
            }
            if (!string.IsNullOrEmpty(filter.MobileNo.Trim()))
            {
                strSql1.AppendFormat(" And MobileNO like '%{0}%' ", filter.MobileNo);
            }
            if (!string.IsNullOrEmpty(filter.ICNo.Trim()))
            {
                strSql1.AppendFormat(" And ICNo like '%{0}%' ", filter.ICNo);
            }
            strSql1.AppendFormat(" And CardFlag = {0} ", filter.CardFlag);

            if (!string.IsNullOrEmpty(filter.SortName.Trim()))
            {
                strSql2.Append(filter.SortName);
                strSql2.Append(" ");
                strSql2.Append(filter.SortOrder.Trim());
            }
            string strWhere = strSql1.ToString();
            string orderby = strSql2.ToString();
            int startIndex = filter.Start;
            int endIndex = startIndex + filter.Length;
            return _daoCustomer.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        public CustomerPO GetCustomerByID(int custID)
        {
            return _daoCustomer.GetModel(custID);
        }

        public void AddCustomer(CustomerPO cust)
        {
            _daoCustomer.Add(cust);
        }

        public void EditCustomer(CustomerPO cust)
        {
            _daoCustomer.Update(cust);
        }

        public void DelCustomer(int id)
        {
            _daoCustomer.Delete(id);
        }
    }
}
