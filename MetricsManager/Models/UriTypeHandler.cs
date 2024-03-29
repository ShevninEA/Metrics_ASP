﻿using Dapper;
using System.Data;
using System.Reflection;

namespace MetricsManager.Models
{
    public class UriTypeHandler : SqlMapper.TypeHandler<Uri> 
    {
        public override void SetValue(IDbDataParameter parameter,Uri value)
        {
            parameter.Value = value.ToString();
        }

        public override Uri Parse(object value)
        {
            return new Uri((string)value);
        }
    }
}