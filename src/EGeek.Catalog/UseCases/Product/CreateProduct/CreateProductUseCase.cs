
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace EGeek.Catalog.UseCases.Product.CreateProduct
{
    public class CreateProductUseCase
    {
        [Authorize]
        public static async Task<Created<string>> Action(
            ClaimsPrincipal claimsPrincipal,
            EGeekCatalogDbContext context,
            IUnitOfWork unitOfWork,
            CreateProductRequest request)
        {
            var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;

            var product = new EGeek.Catalog.Entities.Product(
                request.Name,
                request.Description,
                request.Price,
                request.QuantityInStock
            )
            {
                CreatedBy = email
            };

            await context.Products.AddAsync(product);

            await unitOfWork.SaveChangesAsync();

            return TypedResults.Created($"/product/{product.Id}", product.Id.ToString());
        }
    }
}
