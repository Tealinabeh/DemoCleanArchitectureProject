using DemoBookApp.Contracts;
using DemoBookApp.Core;

namespace DemoBookApp.Infrastructure.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> ResolveQuery(this IQueryable<Book> queryable, BookQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.Title))
                queryable = queryable.Where(b => b.Title == query.Title);

            queryable = queryable.ResolvePriceSort(query)
                                    .ResolveDateSort(query)
                                    .ResolveAuthorSort(query)
                                    .ResolveOrderBy(query);

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return queryable.Skip(skipNumber).Take(query.PageSize);
        }
        private static IQueryable<Book> ResolveOrderBy(this IQueryable<Book> queryable, BookQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.OrderBy))
                return queryable;
            if (query.IsDescending)
            {
                switch (query.OrderBy.ToLower())
                {       
                    case "title":
                        return queryable = queryable.OrderByDescending(b => b.Title);
                    case "author":
                        return queryable = queryable.OrderByDescending(b => b.Author.Surname)
                                        .ThenByDescending(b => b.Author.Name);
                }
            }
            else
            {
                switch (query.OrderBy.ToLower())
                {
                    case "title":
                        return queryable = queryable.OrderBy(b => b.Title);
                    case "author":
                        return queryable = queryable.OrderBy(b => b.Author.Surname)
                                        .ThenBy(b => b.Author.Name);
                }
            }
            return queryable;
        }
        private static IQueryable<Book> ResolveAuthorSort(this IQueryable<Book> queryable, BookQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.AuthorName))
                    queryable = queryable.Where(b => b.Author.Name == query.AuthorName);

            if (!string.IsNullOrWhiteSpace(query.AuthorSurname))
                    queryable = queryable.Where(b => b.Author.Name == query.AuthorSurname);
            return queryable;
        }

        private static IQueryable<Book> ResolveDateSort(this IQueryable<Book> queryable, BookQuery query)
        {
            var dateCheckFlag = false;

            if (query.IssuedAfter is not null)
            {
                queryable = queryable.Where(b => b.DateOfIssue >= query.IssuedAfter);
                dateCheckFlag = true;
            }

            if (query.IssuedBefore is not null)
            {
                if (dateCheckFlag && query.IssuedAfter > query.IssuedBefore)
                    throw new QueryArgumentException($"Issue date filters are incorrect. Impossible to find with parameters:\nBook was issued after {query.IssuedAfter} but before {query.IssuedBefore}");

                queryable = queryable.Where(b => b.DateOfIssue <= query.IssuedBefore);
            }
            return queryable;
        }

        private static IQueryable<Book> ResolvePriceSort(this IQueryable<Book> queryable, BookQuery query)
        {
            var priceCheckFlag = false;

            if (query.LowestPrice is not null)
            {
                queryable = queryable.Where(b => b.Price >= query.LowestPrice);
                priceCheckFlag = true;
            }

            if (query.HighestPrice is not null)
            {
                if (priceCheckFlag && query.HighestPrice < query.LowestPrice)
                    throw new QueryArgumentException("Lowest price of book is higher than the highest price");

                queryable = queryable.Where(b => b.Price <= query.HighestPrice);

            }
            return queryable;
        }
    }
}