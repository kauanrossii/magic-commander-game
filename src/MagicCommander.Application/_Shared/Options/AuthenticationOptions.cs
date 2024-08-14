using System.Text;

namespace MagicCommander.Application._Shared.Options
{
	public class AuthenticationOptions
	{
		private byte[] _jwtSecretBytes = [];
		public string JwtSecret { get; init; } = string.Empty;
		public byte[] JwtSecretBytes
		{
			get
			{
				if (_jwtSecretBytes.Length == 0)
				{
					_jwtSecretBytes = Encoding.ASCII.GetBytes(JwtSecret);
				}
				return _jwtSecretBytes;
			}
			init { _jwtSecretBytes = value; }
		}
	}
}
