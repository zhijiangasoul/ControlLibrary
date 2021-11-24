﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomUserControlLibrary.Model.WYYmodel
{
    public enum SHOW_LRC_TYPE_ENUM
    {
        ONLY_ORIGIN = 0, // 仅显示原文
        ONLY_TRANSLATE = 1, // 仅显示译文
        ORIGIN_PRIOR = 2, // 优先原文
        TRANSLATE_PRIOR = 3, // 优先译文
        MERGE_ORIGIN = 4, // 合并显示，优先原文
        MERGE_TRANSLATE = 5, // 合并显示，优先译文
    }

    // 输出文件名类型
    public enum OUTPUT_FILENAME_TYPE_ENUM
    {
        NAME_SINGER = 0, // 歌曲名 - 歌手
        SINGER_NAME = 1, // 歌手 - 歌曲名
        NAME = 2 // 歌曲名
    }

    // 搜索类型
    public enum SEARCH_TYPE_ENUM
    {
        SONG_ID = 0, // 歌曲ID
        ALBUM_ID = 1 // 专辑ID
    }

    // 输出文件格式
    public enum OUTPUT_ENCODING_ENUM
    {
        UTF_8 = 0,
        UTF_8_BOM = 1,
        GB_2312 = 2,
        GBK = 3
    }

    public class ErrorMsg
    {
        public static string SEARCH_RESULT_STAGE = "查询成功，结果已暂存";
        public static string MUST_SEARCH_BEFORE_SAVE = "您必须先搜索，才能保存内容";
        public static string MUST_SEARCH_BEFORE_COPY_SONG_URL = "您必须先搜索，才能获取直链";
        public static string INPUT_ID_ILLEGAG = "您输入的 ID 号不合法";
        public static string SONG_NOT_EXIST = "歌曲信息暂未被收录";
        public static string LRC_NOT_EXIST = "歌词信息暂未被收录";
        public static string FUNCTION_NOT_SUPPORT = "该功能暂不可用，请等待后续更新";
        public static string SONG_URL_COPY_SUCESS = "歌曲直链，已复制到剪切板";
        public static string BATCH_SONG_URL_COPY_SUCESS = "批量歌曲直链，已复制到剪切板";
        public static string FILE_NAME_IS_EMPTY = "输出文件名不能为空";
        public static string SAVE_SUCCESS = "保存成功";

        public static string GET_LATEST_VERSION_FAILED = "获取最新版本失败";
        public static string THIS_IS_LATEST_VERSION = "当前版本已经是最新版本";
        public static string EXIST_LATEST_VERSION = "检测到最新版本，下载地址已复制到剪切板";
    }

    public class SaveVO
    {
        public SaveVO(SongVO songVO, LyricVO lyricVO)
        {
            this.songVO = songVO;
            this.lyricVO = lyricVO;
        }

        public SongVO songVO { get; set; }

        public LyricVO lyricVO { get; set; }
    }

    public class SongVO
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string Name { get; set; }

        public string Singer { get; set; }

        public string Album { get; set; }

        public string Links { get; set; }
    }

    public class LyricVO
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string Lyric { get; set; }

        public string TLyric { get; set; }

        public string Output { get; set; }
    }

    public class SearchInfo
    {
        public SEARCH_TYPE_ENUM SerchType { get; set; }

        public OUTPUT_FILENAME_TYPE_ENUM OutputFileNameType { get; set; }

        public SHOW_LRC_TYPE_ENUM ShowLrcType { get; set; }

        public string SearchId { get; set; }

        public OUTPUT_ENCODING_ENUM Encoding { get; set; }

        public string LrcMergeSeparator { get; set; }

        public bool BatchSearch { get; set; }

        public bool Constraint2Dot { get; set; }
    }
}
