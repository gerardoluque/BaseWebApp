using System.Threading;
using System.Threading.Tasks;
using API.Application.Core;
using MediatR;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace API.Application.Usuarios.Commands
{
    public class CreateUserAzureAD
    {
        public class Command : IRequest<Result<string>>
        {
            public string DisplayName { get; set; }
            public string Mail { get; set; }
            public string UserPrincipalName { get; set; }
            public string Password { get; set; }
            public bool ForceChangePasswordNextSignIn { get; set; } = true;
            public string MailNickname { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly GraphServiceClient _graphClient;

            public Handler(GraphServiceClient graphClient)
            {
                _graphClient = graphClient;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new User
                {
                    DisplayName = request.DisplayName,
                    Mail = request.Mail,
                    MailNickname = request.MailNickname,
                    UserPrincipalName = request.UserPrincipalName,
                    PasswordProfile = new PasswordProfile
                    {
                        Password = request.Password,
                        ForceChangePasswordNextSignIn = request.ForceChangePasswordNextSignIn
                    },
                    AccountEnabled = true
                };

                var result = await _graphClient.Users.PostAsync(user, cancellationToken: cancellationToken);
                return Result<string>.Success(result.Id);
            }
        }
    }

    public class UpdateUserAzureAD
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
            public string DisplayName { get; set; }
            public string Mail { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly GraphServiceClient _graphClient;

            public Handler(GraphServiceClient graphClient)
            {
                _graphClient = graphClient;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new User
                {
                    DisplayName = request.DisplayName,
                    Mail = request.Mail
                };

                await _graphClient.Users[request.Id].PatchAsync(user, cancellationToken: cancellationToken);
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }

    public class DeleteUserAzureAD
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly GraphServiceClient _graphClient;

            public Handler(GraphServiceClient graphClient)
            {
                _graphClient = graphClient;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                await _graphClient.Users[request.Id].DeleteAsync(cancellationToken: cancellationToken);
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
