﻿namespace conduit.domain.models;

public class Token
{
	public Token(string value)
	{
		Value = value;
	}

    public string Value { get; }
}