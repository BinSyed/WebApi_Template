using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class UpdateProductByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }

        public class UpdateProductByIdCommandHandler :  IRequestHandler<UpdateProductByIdCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateProductByIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(UpdateProductByIdCommand request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Where(u => u.Id == request.Id).FirstOrDefaultAsync();
                if (product == null) return default;

                product.Barcode = request.Barcode;
                product.Name = request.Name;
                product.Rate = request.Rate;
                product.Description = request.Description;
                await _context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}
