﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace FIPAG_KOBOTOOLBOX.Extensions
{
    public static class ModelBuilderExtension
    {
        public static DateTime? ToDateTime(this string dateString, int format) => throw new NotSupportedException();

        public static ModelBuilder AddSqlConvertFunction(this ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(() => ToDateTime(default, default))
                .HasTranslation(args => new SqlFunctionExpression(
                        functionName: "CONVERT",
                        arguments: args.Prepend(new SqlFragmentExpression("date")),
                        nullable: true,
                        argumentsPropagateNullability: new[] { false, true, false },
                        type: typeof(DateTime),
                        typeMapping: null));

            return modelBuilder;
        }
    }
}
