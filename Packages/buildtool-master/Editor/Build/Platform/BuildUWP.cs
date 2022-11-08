﻿using System.Linq;
using UnityEditor;

namespace SuperUnityBuild.BuildTool
{
    [System.Serializable]
    public class BuildUWP : BuildPlatform
    {
        #region Constants

        private const string _name = "UWP";
        private const string _binaryNameFormat = "";
        private const string _dataDirNameFormat = "{0}_Data";
        private const BuildTargetGroup _targetGroup = BuildTargetGroup.WSA;

        private const string _architectureVariantId = "Architecture";
        private const string _buildTypeVariantId = "Build Type";
        private const string _targetDeviceVariantId = "Target Device";
        #endregion

        public BuildUWP()
        {
            enabled = false;
            Init();
        }

        public override void Init()
        {
            platformName = _name;
            dataDirNameFormat = _dataDirNameFormat;
            targetGroup = _targetGroup;

            if (architectures == null || architectures.Length == 0)
            {
                architectures = new BuildArchitecture[] {
                    new BuildArchitecture(BuildTarget.WSAPlayer, "UWP", true, _binaryNameFormat),
                };
            }

            if (variants == null || variants.Length == 0)
            {
                variants = new BuildVariant[] {
                    new BuildVariant(_targetDeviceVariantId, EnumNamesToArray<WSASubtarget>(true).ToArray(), 0),
                    new BuildVariant(_architectureVariantId, new string[] { "x86", "x64", "ARM", "ARM64" }, 0),
                    new BuildVariant(_buildTypeVariantId, EnumNamesToArray<WSAUWPBuildType>(true).ToArray(), 0)
                };
            }
        }

        public override void ApplyVariant()
        {
            foreach (var variantOption in variants)
            {
                string key = variantOption.variantKey;

                switch (variantOption.variantName)
                {
                    case _architectureVariantId:
                        SetArchitecture(key);
                        break;
                    case _buildTypeVariantId:
                        SetBuildType(key);
                        break;
                    case _targetDeviceVariantId:
                        SetTargetDevice(key);
                        break;
                }
            }
        }

        private void SetArchitecture(string key)
        {
            EditorUserBuildSettings.wsaArchitecture = key;
        }

        private void SetBuildType(string key)
        {
            EditorUserBuildSettings.wsaUWPBuildType = EnumValueFromKey<WSAUWPBuildType>(key);
        }

        private void SetTargetDevice(string key)
        {
            EditorUserBuildSettings.wsaSubtarget = EnumValueFromKey<WSASubtarget>(key);
        }
    }
}
