using AutoMapper;
using BokAdventure.Application.Common.Interfaces;
using BokAdventure.Application.Users.Dtos;
using BokAdventure.Domain.Common;
using BokAdventure.Domain.Entities;
using BokAdventure.Domain.Enumerations;
using BokAdventure.Domain.Interfaces;
using BokAdventure.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BokAdventure.Application.Users.Commands;

public sealed record LoginCommand(string UserName, string Password) : ICommand<BokFlow<AccountDto>>;
public sealed class Handler : ICommandHandler<LoginCommand, BokFlow<AccountDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IBokFlowService _bokFlowService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Handler(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService, IBokFlowService bokFlowService, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _mapper = mapper;
        _tokenService = tokenService;
        _bokFlowService = bokFlowService;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<BokFlow<AccountDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .SingleOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken)
            ?? throw new Exception("User Not Found");

        var isCorrectPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isCorrectPassword)
        {
            throw new Exception("Password incorrect");
        }

        var accountDto = _mapper.Map<AccountDto>(user);

        accountDto.Token = _tokenService.GenerateToken(user);

        _httpContextAccessor.HttpContext!.Response.Cookies
            .Append(InfrastructureConstants.CookieUserToken, accountDto.Token);

        var bokFlow = BokFlow<AccountDto>.Create(accountDto);
        var aspBok = await _bokFlowService.Get(BokIdentify.ASPNET);
        var postgreSQLBok = await _bokFlowService.Get(BokIdentify.PostgreSQL);
        bokFlow.AddBok(aspBok);
        bokFlow.AddBok(postgreSQLBok);

        return bokFlow;
    }
}
