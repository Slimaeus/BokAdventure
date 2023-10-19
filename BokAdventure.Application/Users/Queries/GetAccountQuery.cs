using AutoMapper;
using BokAdventure.Application.Common.Interfaces;
using BokAdventure.Application.Users.Dtos;
using BokAdventure.Domain.Common;
using BokAdventure.Domain.Entities;
using BokAdventure.Domain.Enumerations;
using BokAdventure.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BokAdventure.Application.Users.Queries;

public sealed record GetAccountQuery : IQuery<BokFlow<AccountDto>>;
public sealed class Handler : IQueryHandler<GetAccountQuery, BokFlow<AccountDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IUserAccessor _userAccessor;
    private readonly IBokFlowService _bokFlowService;

    public Handler(UserManager<ApplicationUser> userManager, IMapper mapper, ITokenService tokenService, IUserAccessor userAccessor, IBokFlowService bokFlowService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _tokenService = tokenService;
        _userAccessor = userAccessor;
        _bokFlowService = bokFlowService;
    }
    public async Task<BokFlow<AccountDto>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .SingleOrDefaultAsync(x => x.Id == _userAccessor.Id, cancellationToken)
            ?? throw new Exception("User Not Found");
        var accountDto = _mapper.Map<AccountDto>(user);

        accountDto.Token = _userAccessor.JwtToken;

        var bokFlow = BokFlow<AccountDto>.Create(accountDto);
        var aspBok = await _bokFlowService.Get(BokIdentify.ASPNET);
        var postgreSQLBok = await _bokFlowService.Get(BokIdentify.PostgreSQL);
        bokFlow.AddBok(aspBok);
        bokFlow.AddBok(postgreSQLBok);

        return bokFlow;
    }
}
