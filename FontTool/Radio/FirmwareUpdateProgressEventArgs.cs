using System;

namespace Radio;

public class FirmwareUpdateProgressEventArgs : EventArgs
{
	public float Percentage;

	public string Message;

	public bool Failed;

	public bool Closed;

	public FirmwareUpdateProgressEventArgs(float Percentage, string Message, bool Failed, bool Closed)
	{
		this.Percentage = Percentage;
		this.Message = Message;
		this.Failed = Failed;
		this.Closed = Closed;
	}
}
