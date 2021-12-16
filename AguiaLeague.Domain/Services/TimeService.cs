using AguiaLeague.Domain.Interfaces.Repositories;
using AguiaLeague.Domain.Interfaces.Services;
using AguiaLeague.Domain.Models;
using AguiaLeague.Domain.Validations.Time;
using FluentValidation.Results;

namespace AguiaLeague.Domain.Services;

public class TimeService : ITimeService
{
    private readonly ITimeRepository _timeRepository;

    public TimeService(ITimeRepository timeRepository)
    {
        _timeRepository = timeRepository;
    }

    public ValidationResult Adicionar(Time time)
    {
        var resultadoValidacao = new AddTimeValidation().Validate(time);
        if (resultadoValidacao.IsValid)
            _timeRepository.Adicionar(time);

        return resultadoValidacao;
    }

    public Time? ObterPorId(Guid id, string[]? includes = null)
    {
        return _timeRepository.ObterPorId(id, includes);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}