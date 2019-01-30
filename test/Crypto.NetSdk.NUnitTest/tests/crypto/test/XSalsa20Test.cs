using System;

using NUnit.Framework;

using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Utilities.Test;

namespace Org.BouncyCastle.Crypto.Tests
{
	/**
	* XSalsa20 Test
	*/
	[TestFixture]
	public class XSalsa20Test 
		: SimpleTest
	{
		private class TestCase
		{

			private byte[] key;
			private byte[] iv;
			private byte[] plaintext;
			private byte[] ciphertext;

			public TestCase(String key, string iv, string plaintext, string ciphertext)
			{
				this.key = Hex.Decode(key);
				this.iv = Hex.Decode(iv);
				this.plaintext = Hex.Decode(plaintext);
				this.ciphertext = Hex.Decode(ciphertext);
			}

			public byte[] Key
			{
				get { return key; }
			}

			public byte[] Iv
			{
				get { return iv; }
			}

			public byte[] Plaintext
			{
				get { return plaintext; }
			}

			public byte[] Ciphertext
			{
				get { return ciphertext; }
			}
		}

		// Test cases generated by naclcrypto-20090308, as used by cryptopp
		private static readonly TestCase[] TEST_CASES = new TestCase[] {
			new TestCase(
				"a6a7251c1e72916d11c2cb214d3c252539121d8e234e652d651fa4c8cff88030",
				"9e645a74e9e0a60d8243acd9177ab51a1beb8d5a2f5d700c",
				"093c5e5585579625337bd3ab619d615760d8c5b224a85b1d0efe0eb8a7ee163abb0376529fcc09bab506c618e13ce777d82c3ae9d1a6f972d4160287cbfe60bf2130fc0a6ff6049d0a5c8a82f429231f008082e845d7e189d37f9ed2b464e6b919e6523a8c1210bd52a02a4c3fe406d3085f5068d1909eeeca6369abc981a42e87fe665583f0ab85ae71f6f84f528e6b397af86f6917d9754b7320dbdc2fea81496f2732f532ac78c4e9c6cfb18f8e9bdf74622eb126141416776971a84f94d156beaf67aecbf2ad412e76e66e8fad7633f5b6d7f3d64b5c6c69ce29003c6024465ae3b89be78e915d88b4b5621d",
				"b2af688e7d8fc4b508c05cc39dd583d6714322c64d7f3e63147aede2d9534934b04ff6f337b031815cd094bdbc6d7a92077dce709412286822ef0737ee47f6b7ffa22f9d53f11dd2b0a3bb9fc01d9a88f9d53c26e9365c2c3c063bc4840bfc812e4b80463e69d179530b25c158f543191cff993106511aa036043bbc75866ab7e34afc57e2cce4934a5faae6eabe4f221770183dd060467827c27a354159a081275a291f69d946d6fe28ed0b9ce08206cf484925a51b9498dbde178ddd3ae91a8581b91682d860f840782f6eea49dbb9bd721501d2c67122dea3b7283848c5f13e0c0de876bd227a856e4de593a3"),
			new TestCase(
				"9e1da239d155f52ad37f75c7368a536668b051952923ad44f57e75ab588e475a",
				"af06f17859dffa799891c4288f6635b5c5a45eee9017fd72",
				"feac9d54fc8c115ae247d9a7e919dd76cfcbc72d32cae4944860817cbdfb8c04e6b1df76a16517cd33ccf1acda9206389e9e318f5966c093cfb3ec2d9ee2de856437ed581f552f26ac2907609df8c613b9e33d44bfc21ff79153e9ef81a9d66cc317857f752cc175fd8891fefebb7d041e6517c3162d197e2112837d3bc4104312ad35b75ea686e7c70d4ec04746b52ff09c421451459fb59f",
				"2c261a2f4e61a62e1b27689916bf03453fcbc97bb2af6f329391ef063b5a219bf984d07d70f602d85f6db61474e9d9f5a2deecb4fcd90184d16f3b5b5e168ee03ea8c93f3933a22bc3d1a5ae8c2d8b02757c87c073409052a2a8a41e7f487e041f9a49a0997b540e18621cad3a24f0a56d9b19227929057ab3ba950f6274b121f193e32e06e5388781a1cb57317c0ba6305e910961d01002f0"),
			new TestCase("d5c7f6797b7e7e9c1d7fd2610b2abf2bc5a7885fb3ff78092fb3abe8986d35e2",
	             "744e17312b27969d826444640e9c4a378ae334f185369c95",
	             "7758298c628eb3a4b6963c5445ef66971222be5d1a4ad839715d1188071739b77cc6e05d5410f963a64167629757",
	             "27b8cfe81416a76301fd1eec6a4d99675069b2da2776c360db1bdfea7c0aa613913e10f7a60fec04d11e65f2d64e"),
			new TestCase(
				"737d7811ce96472efed12258b78122f11deaec8759ccbd71eac6bbefa627785c",
				"6fb2ee3dda6dbd12f1274f126701ec75c35c86607adb3edd",
				"501325fb2645264864df11faa17bbd58312b77cad3d94ac8fb8542f0eb653ad73d7fce932bb874cb89ac39fc47f8267cf0f0c209f204b2d8578a3bdf461cb6a271a468bebaccd9685014ccbc9a73618c6a5e778a21cc8416c60ad24ddc417a130d53eda6dfbfe47d09170a7be1a708b7b5f3ad464310be36d9a2a95dc39e83d38667e842eb6411e8a23712297b165f690c2d7ca1b1346e3c1fccf5cafd4f8be0",
				"6724c372d2e9074da5e27a6c54b2d703dc1d4c9b1f8d90f00c122e692ace7700eadca942544507f1375b6581d5a8fb39981c1c0e6e1ff2140b082e9ec016fce141d5199647d43b0b68bfd0fea5e00f468962c7384dd6129aea6a3fdfe75abb210ed5607cef8fa0e152833d5ac37d52e557b91098a322e76a45bbbcf4899e790618aa3f4c2e5e0fc3de93269a577d77a5502e8ea02f717b1dd2df1ec69d8b61ca"),
			new TestCase(
				"760158da09f89bbab2c99e6997f9523a95fcef10239bcca2573b7105f6898d34",
				"43636b2cc346fc8b7c85a19bf507bdc3dafe953b88c69dba",
				"d30a6d42dff49f0ed039a306bae9dec8d9e88366cc19e8c3642fd58fa0794ebf8029d949730339b0823a51f0f49f0d2c71f1051c1e0e2c86941f172789cdb1b0107413e70f982ff9761877bb526ef1c3eb1106a948d60ef21bd35d32cfd64f89b79ed63ecc5cca56246af736766f285d8e6b0da9cb1cd21020223ffacc5a32",
				"c815b6b79b64f9369aec8dce8c753df8a50f2bc97c70ce2f014db33a65ac5816bac9e30ac08bdded308c65cb87e28e2e71b677dc25c5a6499c1553555daf1f55270a56959dffa0c66f24e0af00951ec4bb59ccc3a6c5f52e0981647e53e439313a52c40fa7004c855b6e6eb25b212a138e843a9ba46edb2a039ee82a263abe"),
			new TestCase(
				"27ba7e81e7edd4e71be53c07ce8e633138f287e155c7fa9e84c4ad804b7fa1b9",
				"ea05f4ebcd2fb6b000da0612861ba54ff5c176fb601391aa",
				"e09ff5d2cb050d69b2d42494bde5825238c756d6991d99d7a20d1ef0b83c371c89872690b2fc11d5369f4fc4971b6d3d6c078aef9b0f05c0e61ab89c025168054defeb03fef633858700c58b1262ce011300012673e893e44901dc18eee3105699c44c805897bdaf776af1833162a21a",
				"a23e7ef93c5d0667c96d9e404dcbe6be62026fa98f7a3ff9ba5d458643a16a1cef7272dc6097a9b52f35983557c77a11b314b4f7d5dc2cca15ee47616f861873cbfed1d32372171a61e38e447f3cf362b3abbb2ed4170d89dcb28187b7bfd206a3e026f084a7e0ed63d319de6bc9afc0"),
			new TestCase("6799d76e5ffb5b4920bc2768bafd3f8c16554e65efcf9a16f4683a7a06927c11",
	             "61ab951921e54ff06d9b77f313a4e49df7a057d5fd627989", "472766", "8fd7df"),
			new TestCase(
				"f68238c08365bb293d26980a606488d09c2f109edafa0bbae9937b5cc219a49c",
				"5190b51e9b708624820b5abdf4e40fad1fb950ad1adc2d26",
				"47ec6b1f73c4b7ff5274a0bfd7f45f864812c85a12fbcb3c2cf8a3e90cf66ccf2eacb521e748363c77f52eb426ae57a0c6c78f75af71284569e79d1a92f949a9d69c4efc0b69902f1e36d7562765543e2d3942d9f6ff5948d8a312cff72c1afd9ea3088aff7640bfd265f7a9946e606abc77bcedae6bddc75a0dba0bd917d73e3bd1268f727e0096345da1ed25cf553ea7a98fea6b6f285732de37431561ee1b3064887fbcbd71935e02",
				"36160e88d3500529ba4edba17bc24d8cfaca9a0680b3b1fc97cf03f3675b7ac301c883a68c071bc54acdd3b63af4a2d72f985e51f9d60a4c7fd481af10b2fc75e252fdee7ea6b6453190617dcc6e2fe1cd56585fc2f0b0e97c5c3f8ad7eb4f31bc4890c03882aac24cc53acc1982296526690a220271c2f6e326750d3fbda5d5b63512c831f67830f59ac49aae330b3e0e02c9ea0091d19841f1b0e13d69c9fbfe8a12d6f30bb734d9d2"),
			new TestCase(
				"45b2bd0de4ed9293ec3e26c4840faaf64b7d619d51e9d7a2c7e36c83d584c3df",
				"546c8c5d6be8f90952cab3f36d7c1957baaa7a59abe3d7e5",
				"5007c8cd5b3c40e17d7fe423a87ae0ced86bec1c39dc07a25772f3e96dabd56cd3fd7319f6c9654925f2d87087a700e1b130da796895d1c9b9acd62b266144067d373ed51e787498b03c52faad16bb3826fa511b0ed2a19a8663f5ba2d6ea7c38e7212e9697d91486c49d8a000b9a1935d6a7ff7ef23e720a45855481440463b4ac8c4f6e7062adc1f1e1e25d3d65a31812f58a71160",
				"8eacfba568898b10c0957a7d44100685e8763a71a69a8d16bc7b3f88085bb9a2f09642e4d09a9f0ad09d0aad66b22610c8bd02ff6679bb92c2c026a216bf425c6be35fb8dae7ff0c72b0efd6a18037c70eed0ca90062a49a3c97fdc90a8f9c2ea536bfdc41918a7582c9927fae47efaa3dc87967b7887dee1bf071734c7665901d9105dae2fdf66b4918e51d8f4a48c60d19fbfbbcba"),
			new TestCase(
				"fe559c9a282beb40814d016d6bfcb2c0c0d8bf077b1110b8703a3ce39d70e0e1",
				"b076200cc7011259805e18b304092754002723ebec5d6200",
				"6db65b9ec8b114a944137c821fd606be75478d928366d5284096cdef782fcff7e8f59cb8ffcda979757902c5ffa6bc477ceaa4cb5d5ea76f94d91e833f823a6bc78f1055dfa6a97bea8965c1cde67a668e001257334a585727d9e0f7c1a06e88d3d25a4e6d9096c968bf138e116a3ebeffd4bb4808adb1fd698164ba0a35c709a47f16f1f4435a2345a9194a00b95abd51851d505809a6077da9baca5831afff31578c487ee68f2767974a98a7e803aac788da98319c4ea8eaa3d394855651f484cef543f537e35158ee29",
				"4dce9c8f97a028051b0727f34e1b9ef21f06f0760f36e71713204027902090ba2bb6b13436ee778d9f50530efbd7a32b0d41443f58ccaee781c7b716d3a96fdec0e3764ed7959f34c3941278591ea033b5cbadc0f1916032e9bebbd1a8395b83fb63b1454bd775bd20b3a2a96f951246ac14daf68166ba62f6cbff8bd121ac9498ff8852fd2be975df52b5daef3829d18eda42e715022dcbf930d0a789ee6a146c2c7088c35773c63c06b4af4559856ac199ced86863e4294707825337c5857970eb7fddeb263781309011"),
			new TestCase(
				"0ae10012d7e56614b03dcc89b14bae9242ffe630f3d7e35ce8bbb97bbc2c92c3",
				"f96b025d6cf46a8a12ac2af1e2aef1fb83590adadaa5c5ea",
				"ea0f354e96f12bc72bbaa3d12b4a8ed879b042f0689878f46b651cc4116d6f78409b11430b3aaa30b2076891e8e1fa528f2fd169ed93dc9f84e24409eec2101daf4d057be2492d11de640cbd7b355ad29fb70400fffd7cd6d425abeeb732a0eaa4330af4c656252c4173deab653eb85c58462d7ab0f35fd12b613d29d473d330310dc323d3c66348bbdbb68a326324657cae7b77a9e34358f2cec50c85609e73056856796e3be8d62b6e2fe9f953",
				"e8abd48924b54e5b80866be7d4ebe5cf4274cafff08b39cb2d40a8f0b472398aedc776e0793812fbf1f60078635d2ed86b15efcdba60411ee23b07233592a44ec31b1013ce8964236675f8f183aef885e864f2a72edf4215b5338fa2b54653dfa1a8c55ce5d95cc605b9b311527f2e3463ffbec78a9d1d65dabad2f338769c9f43f133a791a11c7eca9af0b771a4ac32963dc8f631a2c11217ac6e1b9430c1aae1ceebe22703f429998a8fb8c641"),
			new TestCase(
				"082c539bc5b20f97d767cd3f229eda80b2adc4fe49c86329b5cd6250a9877450",
				"845543502e8b64912d8f2c8d9fffb3c69365686587c08d0c",
				"a96bb7e910281a6dfad7c8a9c370674f0ceec1ad8d4f0de32f9ae4a23ed329e3d6bc708f876640a229153ac0e7281a8188dd77695138f01cda5f41d5215fd5c6bdd46d982cb73b1efe2997970a9fdbdb1e768d7e5db712068d8ba1af6067b5753495e23e6e1963af012f9c7ce450bf2de619d3d59542fb55f3",
				"835da74fc6de08cbda277a7966a07c8dcd627e7b17adde6d930b6581e3124b8baad096f693991fedb1572930601fc7709541839b8e3ffd5f033d2060d999c6c6e3048276613e648000acb5212cc632a916afce290e20ebdf612d08a6aa4c79a74b070d3f872a861f8dc6bb07614db515d363349d3a8e3336a3"),
			new TestCase("3d02bff3375d403027356b94f514203737ee9a85d2052db3e4e5a217c259d18a",
	             "74216c95031895f48c1dba651555ebfa3ca326a755237025",
	             "0d4b0f54fd09ae39baa5fa4baccf2e6682e61b257e01f42b8f",
	             "16c4006c28365190411eb1593814cf15e74c22238f210afc3d"),
			new TestCase(
				"ad1a5c47688874e6663a0f3fa16fa7efb7ecadc175c468e5432914bdb480ffc6",
				"e489eed440f1aae1fac8fb7a9825635454f8f8f1f52e2fcc",
				"aa6c1e53580f03a9abb73bfdadedfecada4c6b0ebe020ef10db745e54ba861caf65f0e40dfc520203bb54d29e0a8f78f16b3f1aa525d6bfa33c54726e59988cfbec78056",
				"02fe84ce81e178e7aabdd3ba925a766c3c24756eefae33942af75e8b464556b5997e616f3f2dfc7fce91848afd79912d9fb55201b5813a5a074d2c0d4292c1fd441807c5"),
			new TestCase(
				"053a02bedd6368c1fb8afc7a1b199f7f7ea2220c9a4b642a6850091c9d20ab9c",
				"c713eea5c26dad75ad3f52451e003a9cb0d649f917c89dde",
				"8f0a8a164760426567e388840276de3f95cb5e3fadc6ed3f3e4fe8bc169d9388804dcb94b6587dbb66cb0bd5f87b8e98b52af37ba290629b858e0e2aa7378047a26602",
				"516710e59843e6fbd4f25d0d8ca0ec0d47d39d125e9dad987e0518d49107014cb0ae405e30c2eb3794750bca142ce95e290cf95abe15e822823e2e7d3ab21bc8fbd445"),
			new TestCase(
				"5b14ab0fbed4c58952548a6cb1e0000cf4481421f41288ea0aa84add9f7deb96",
				"54bf52b911231b952ba1a6af8e45b1c5a29d97e2abad7c83",
				"37fb44a675978b560ff9a4a87011d6f3ad2d37a2c3815b45a3c0e6d1b1d8b1784cd468927c2ee39e1dccd4765e1c3d676a335be1ccd6900a45f5d41a317648315d8a8c24adc64eb285f6aeba05b9029586353d303f17a807658b9ff790474e1737bd5fdc604aeff8dfcaf1427dcc3aacbb0256badcd183ed75a2dc52452f87d3c1ed2aa583472b0ab91cda20614e9b6fdbda3b49b098c95823cc72d8e5b717f2314b0324e9ce",
				"ae6deb5d6ce43d4b09d0e6b1c0e9f46157bcd8ab50eaa3197ff9fa2bf7af649eb52c68544fd3adfe6b1eb316f1f23538d470c30dbfec7e57b60cbcd096c782e7736b669199c8253e70214cf2a098fda8eac5da79a9496a3aae754d03b17c6d70d1027f42bf7f95ce3d1d9c338854e158fcc803e4d6262fb639521e47116ef78a7a437ca9427ba645cd646832feab822a208278e45e93e118d780b988d65397eddfd7a819526e"),
			new TestCase(
				"d74636e3413a88d85f322ca80fb0bd650bd0bf0134e2329160b69609cd58a4b0",
				"efb606aa1d9d9f0f465eaa7f8165f1ac09f5cb46fecf2a57",
				"f85471b75f6ec81abac2799ec09e98e280b2ffd64ca285e5a0109cfb31ffab2d617b2c2952a2a8a788fc0da2af7f530758f74f1ab56391ab5ff2adbcc5be2d6c7f49fbe8118104c6ff9a23c6dfe52f57954e6a69dcee5db06f514f4a0a572a9a8525d961dae72269b987189d465df6107119c7fa790853e063cba0fab7800ca932e258880fd74c33c784675bedad0e7c09e9cc4d63dd5e9713d5d4a0196e6b562226ac31b4f57c04f90a181973737ddc7e80f364112a9fbb435ebdbcabf7d490ce52",
				"b2b795fe6c1d4c83c1327e015a67d4465fd8e32813575cbab263e20ef05864d2dc17e0e4eb81436adfe9f638dcc1c8d78f6b0306baf938e5d2ab0b3e05e735cc6fff2d6e02e3d60484bea7c7a8e13e23197fea7b04d47d48f4a4e5944174539492800d3ef51e2ee5e4c8a0bdf050c2dd3dd74fce5e7e5c37364f7547a11480a3063b9a0a157b15b10a5a954de2731ced055aa2e2767f0891d4329c426f3808ee867bed0dc75b5922b7cfb895700fda016105a4c7b7f0bb90f029f6bbcb04ac36ac16") };

		public override string Name
		{
			get { return "XSalsa20"; }
		}

		public override void PerformTest()
		{
			for (int i = 0; i < TEST_CASES.Length; i++)
			{
				performTest(i, TEST_CASES[i]);
			}
		}

		private void performTest(int number, TestCase testCase)
		{
			byte[] plaintext = testCase.Plaintext;
			byte[] output = new byte[plaintext.Length];

			XSalsa20Engine engine = new XSalsa20Engine();
			engine.Init(false, new ParametersWithIV(new KeyParameter(testCase.Key), testCase.Iv));

			engine.ProcessBytes(testCase.Plaintext, 0, testCase.Plaintext.Length, output, 0);

			if (!Arrays.AreEqual(testCase.Ciphertext, output))
			{
				Fail("mismatch on " + number, Hex.ToHexString(testCase.Ciphertext), Hex.ToHexString(output));
			}
		}

		public  static void RunMainTests(
			string[] args)
		{
			RunTest(new XSalsa20Test());
		}

		[Test]
		public void TestFunction()
		{
			string resultText = Perform().ToString();

			Assert.AreEqual(Name + ": Okay", resultText);
		}
	}
}
