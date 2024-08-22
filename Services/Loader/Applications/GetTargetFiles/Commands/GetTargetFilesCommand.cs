using MediatR;

namespace Loader.Applications.GetTargetFiles.Commands;

internal sealed record GetTargetFilesCommand(long Id) : IRequest;
