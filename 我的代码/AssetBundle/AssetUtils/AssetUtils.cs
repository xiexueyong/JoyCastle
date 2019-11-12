/*-------------------------------------------------------------------------------------------
// 模块名：AssetUtils
// 模块描述：assetbundle模块用到的工具类
//-------------------------------------------------------------------------------------------*/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

namespace Framework.Asset
{
    public static class AssetUtils
    {
        #region MD5文件校验

        public static string EncryptWithMD5(string source)
        {
            byte[] sor = Encoding.UTF8.GetBytes(source);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                strbul.Append(result[i].ToString("x2"));//加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
            }
            return strbul.ToString();
        }

        // 生成文件的md5
        public static String BuildFileMd5(String filename)
        {
            String filemd5 = null;
            if (File.Exists(filename))
            {
                try
                {
                    using (var fileStream = File.OpenRead(filename))
                    {
                        var md5 = MD5.Create();
                        var fileMD5Bytes = md5.ComputeHash(fileStream);
                        filemd5 = FormatMD5(fileMD5Bytes);
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.ToString());
                }
            }
            return filemd5;
        }

        // 文件的体积，以K为单位
        public static int FileSize(String filename)
        {
            if (File.Exists(filename))
            {
                FileInfo fileInfo = new FileInfo(filename);
                return (int)(fileInfo.Length / 1024);
            }
            return 0;
        }

        public static Byte[] CreateMD5(Byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(data);
            }
        }

        public static string FormatMD5(Byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", "").ToLower();
        }
        #endregion

        public static void Empty(this DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles()) file.Delete();
            foreach (DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }


        public static AssetBundle LoadLocalAssetBundle(string path)
        {
            AssetBundle ab = AssetBundle.LoadFromFile(path);
            if (ab == null)
            {
                DebugUtil.LogError("AssetBundle __{0}__ is not exist", path);
            }
            return ab;
        }
    }
}
