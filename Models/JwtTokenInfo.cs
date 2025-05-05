﻿using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace StudentMangementSystemC8.Models
{
    public class JwtTokenInfo
    {
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required int ExpiresInMinutes { get; set; }
        public required string Key { get; set; }
    }
}
