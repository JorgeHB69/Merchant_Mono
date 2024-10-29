using InventoryService.Application.Dtos.Category;
using InventoryService.Domain.Concretes;
using MediatR;

namespace InventoryService.Application.QueryCommands.Categories.Commands.Commands;

public class CreateCategoryCommand(CreateCategoryDto createCategoryDto) : IRequest<Category>
{
    public CreateCategoryDto CreateCategoryDto { get; set; } = createCategoryDto;
}