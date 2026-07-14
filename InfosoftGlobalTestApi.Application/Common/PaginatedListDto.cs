using System;
using System.Collections.Generic;
using System.Text;

namespace InfosoftGlobalTestApi.Application.Common
{
    public record PaginatedListDto<T>
    (
    IReadOnlyCollection<T> Items,
    int TotalCount,
    int PageNumber,
    int PageSize
    );
}
