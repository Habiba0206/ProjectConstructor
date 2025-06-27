using PageConstructor.Application.Projects.Models;
using PageConstructor.Application.Projects.Services;
using PageConstructor.Domain.Common.Commands;
using PageConstructor.Domain.Common.Exceptions;
using PageConstructor.Domain.Common.Queries;
using PageConstructor.Domain.Entities;
using PageConstructor.Persistence.Extensions;
using PageConstructor.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PageConstructor.Infrastructure.Projects.Services;

public class ProjectService(
    IProjectRepository projectRepository)
   : IProjectService
{
    public IQueryable<Project> Get(
        Expression<Func<Project, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    projectRepository.Get(predicate, queryOptions);

    public IQueryable<Project> Get(
        ProjectFilter projectFilter,
        QueryOptions queryOptions = default) =>
    projectRepository
        .Get(queryOptions: queryOptions)
        .ApplyPagination(projectFilter);

    public ValueTask<Project?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    projectRepository.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Project>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    projectRepository.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    projectRepository.CheckByIdAsync(id, cancellationToken);

    public async ValueTask<Project> CreateAsync(
        Project project,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    await projectRepository.CreateAsync(project, commandOptions, cancellationToken);

    public async ValueTask<Project> UpdateAsync(
        Project project,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var existingProject = await projectRepository.GetByIdAsync(project.Id) ?? throw new NotFoundException(typeof(Project).Name, project.Id);

        existingProject.Name = project.Name;
        existingProject.UrlPath = project.UrlPath;
        existingProject.GlobalStyles = project.GlobalStyles;

        return await projectRepository.UpdateAsync(existingProject, commandOptions, cancellationToken);
    }

    public async ValueTask<Project> PatchAsync(
        ProjectPatchDto patchDto,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var existing = await projectRepository.GetByIdAsync(patchDto.Id, cancellationToken: cancellationToken)
                      ?? throw new NotFoundException(typeof(Project).Name, patchDto.Id);

        if (patchDto.Name is not null) existing.Name = patchDto.Name;
        if (patchDto.UrlPath is not null) existing.UrlPath = patchDto.UrlPath;
        if (patchDto.GlobalStyles is not null) existing.GlobalStyles = patchDto.GlobalStyles;

        return await projectRepository.UpdateAsync(existing, cancellationToken: cancellationToken);
    }

    public ValueTask<Project?> DeleteAsync(
        Project project,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    projectRepository.DeleteAsync(project, commandOptions, cancellationToken);

    public ValueTask<Project?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    projectRepository.DeleteByIdAsync(id, commandOptions, cancellationToken);
}