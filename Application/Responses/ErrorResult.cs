﻿using Application.Localization;

namespace Application.Responses;

public record ErrorResult : Result
{
    public ErrorResult(string message)
        : base(false, message)
    {
    }

    public ErrorResult()
        : base(false, EMessages.GeneralError.Translate())
    {
    }
}