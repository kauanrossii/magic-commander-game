using System.Text;

namespace MagicCommander.Application._Shared.Options
{
	public class JwtOptions
	{
		private byte[] _jwtSecretBytes = [];
		public string JwtSecret { get; init; } = string.Empty;
		public string JwtAudience { get; init; } = string.Empty;
		public string JwtIssuer { get; set; } = string.Empty;

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
