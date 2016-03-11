using System;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using PCLCrypto;
using JosePCL.Util;

namespace JosePCL.Jws
{
    public sealed class RsaUsingSha : IJwsSigner
    {
        private int keySizeBits;

        public RsaUsingSha(int keySizeBits)
        {
            this.keySizeBits = keySizeBits;
        }

        public byte[] Sign([ReadOnlyArray] byte[] securedInput, object key)
        {            
            var publicKey = Ensure.Type<ICryptographicKey>(key, "RsaUsingSha expects key to be of type 'ICryptographicKey'");

            //reattach key to alg provider
            byte[] keyBlob = publicKey.Export(CryptographicPrivateKeyBlobType.BCryptPrivateKey);

            ICryptographicKey cKey = AlgProvider.ImportKeyPair(keyBlob, CryptographicPrivateKeyBlobType.BCryptPrivateKey);

            return WinRTCrypto.CryptographicEngine.Sign(cKey, securedInput);
        }

        public bool Verify([ReadOnlyArray] byte[] signature, [ReadOnlyArray] byte[] securedInput, object key)
        {
            var publicKey = Ensure.Type<ICryptographicKey>(key, "RsaUsingSha expects key to be of type 'ICryptographicKey'");
            
            //reattach key to alg provider
            byte[] keyBlob = publicKey.ExportPublicKey(CryptographicPublicKeyBlobType.BCryptPublicKey);

            ICryptographicKey cKey = AlgProvider.ImportPublicKey(keyBlob, CryptographicPublicKeyBlobType.BCryptPublicKey);

            return WinRTCrypto.CryptographicEngine.VerifySignature(cKey, securedInput, signature);
        }

        public string Name
        {
            get
            {
                switch (keySizeBits)
                {
                    case 256: return JwsAlgorithms.RS256;
                    case 384: return JwsAlgorithms.RS384;
                    default: return JwsAlgorithms.RS512;
                }
            }
        }

        private IAsymmetricKeyAlgorithmProvider AlgProvider
        {
            get
            {
                switch (keySizeBits)
                {
                    case 256: return WinRTCrypto.AsymmetricKeyAlgorithmProvider.OpenAlgorithm(AsymmetricAlgorithm.RsaSignPkcs1Sha256);
                    case 384: return WinRTCrypto.AsymmetricKeyAlgorithmProvider.OpenAlgorithm(AsymmetricAlgorithm.RsaSignPkcs1Sha384);
                    default: return WinRTCrypto.AsymmetricKeyAlgorithmProvider.OpenAlgorithm(AsymmetricAlgorithm.RsaSignPkcs1Sha512);
                }
            }
        }
    }
}