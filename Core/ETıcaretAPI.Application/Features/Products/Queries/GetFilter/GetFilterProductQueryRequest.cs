using ETıcaretAPI.Application.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Application.Features.Products.Queries.GetFilter
{
    public class GetFilterProductQueryRequest:IRequest<GetFilterProductQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
        public string? Category { get; set; } = null;
        public string? SortOrder { get; set; }=null;
        public string? MinPrice { get; set; } = null;
        public string? MaxPrice { get; set; } = null;
    }
    public class GetFilterProductQueryResponse
    {
        public int TotalProductCount { get; set; }
        public object Products { get; set; }
    }
    public class GetFilterProductQueryHandler : IRequestHandler<GetFilterProductQueryRequest, GetFilterProductQueryResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;


        public GetFilterProductQueryHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<GetFilterProductQueryResponse> Handle(GetFilterProductQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _productReadRepository.Table.Include(p => p.ProductImageFiles)
                                                      .Include(p => p.Category).AsQueryable();

            if (!String.IsNullOrEmpty(request.Category))
            {
                query = query.Where(p => p.Category.Name == request.Category);
            }
            // Kategori verilmediğinde filtreleme yapma
            // Kategori null olduğunda tüm ürünleri getir
            // Bu durumda ek bir koşula gerek yok
            if (!String.IsNullOrEmpty(request.SortOrder))
            {
                if (request.SortOrder.ToLower() == "desc")
                {
                    query = query.OrderByDescending(p => p.Price);
                }
                else if (request.SortOrder.ToLower() == "asc")
                {
                    query = query.OrderBy(p => p.Price);
                }
            }
            if (!String.IsNullOrEmpty(request.MinPrice))
            {
                query = query.Where(p => p.Price > float.Parse(request.MinPrice));
            }
            if (!String.IsNullOrEmpty(request.MaxPrice))
            {
                query = query.Where(p => p.Price < float.Parse(request.MaxPrice));
            }

            var data = await query.Skip(request.Page * request.Size)
                                 .Take(request.Size)
                                 .Select(d => new
                                 {
                                     d.Id,
                                     d.Name,
                                     d.Stock,
                                     d.Price,
                                     d.CreatedDate,
                                     d.UpdatedDate,
                                     d.ProductImageFiles,
                                     categoryName = d.Category.Name
                                 }).ToListAsync();

            return new GetFilterProductQueryResponse
            {
                Products = data,
                TotalProductCount = await query.CountAsync(),
            };
        }


    }
}
