// UniTube - An open source client for YouTube
// Copyright (C) 2019 Nucleux Software
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
// Author: Nahuel Gomez Castro <nahual_gomca@outlook.com.ar>

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UniTube.Core.Data
{
    public class Video
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("snippet")]
        public Snippet Snippet { get; set; }

        [JsonProperty("contentDetails")]
        public ContentDetails ContentDetails { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("statistics")]
        public Statistics Statistics { get; set; }

        [JsonProperty("player")]
        public Player Player { get; set; }

        [JsonProperty("topicDetails")]
        public TopicDetails TopicDetails { get; set; }

        [JsonProperty("recordingDetails")]
        public RecordingDetails RecordingDetails { get; set; }

        [JsonProperty("fileDetails")]
        public FileDetails FileDetails { get; set; }

        [JsonProperty("processingDetails")]
        public ProcessingDetails ProcessingDetails { get; set; }

        [JsonProperty("suggestions")]
        public Suggestions Suggestions { get; set; }

        [JsonProperty("liveStreamingDetails")]
        public LiveStreamingDetails LiveStreamingDetails { get; set; }

        [JsonProperty("localizations")]
        public Dictionary<string, Localization> Localizations { get; set; }

        public static Video FromJson(string json)
            => JsonConvert.DeserializeObject<Video>(json);
    }

    public class ContentDetails
    {
        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("dimension")]
        public string Dimension { get; set; }

        [JsonProperty("definition")]
        public string Definition { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("licensedContent")]
        public bool LicensedContent { get; set; }

        [JsonProperty("regionRestriction")]
        public RegionRestriction RegionRestriction { get; set; }

        [JsonProperty("contentRating")]
        public ContentRating ContentRating { get; set; }

        [JsonProperty("projection")]
        public string Projection { get; set; }

        [JsonProperty("hasCustomThumbnail")]
        public bool HasCustomThumbnail { get; set; }
    }

    public class ContentRating
    {
        [JsonProperty("acbRating")]
        public string AcbRating { get; set; }

        [JsonProperty("agcomRating")]
        public string AgcomRating { get; set; }

        [JsonProperty("anatelRating")]
        public string AnatelRating { get; set; }

        [JsonProperty("bbfcRating")]
        public string BbfcRating { get; set; }

        [JsonProperty("bfvcRating")]
        public string BfvcRating { get; set; }

        [JsonProperty("bmukkRating")]
        public string BmukkRating { get; set; }

        [JsonProperty("catvRating")]
        public string CatvRating { get; set; }

        [JsonProperty("catvfrRating")]
        public string CatvfrRating { get; set; }

        [JsonProperty("cbfcRating")]
        public string CbfcRating { get; set; }

        [JsonProperty("cccRating")]
        public string CccRating { get; set; }

        [JsonProperty("cceRating")]
        public string CceRating { get; set; }

        [JsonProperty("chfilmRating")]
        public string ChfilmRating { get; set; }

        [JsonProperty("chvrsRating")]
        public string ChvrsRating { get; set; }

        [JsonProperty("cicfRating")]
        public string CicfRating { get; set; }

        [JsonProperty("cnaRating")]
        public string CnaRating { get; set; }

        [JsonProperty("cncRating")]
        public string CncRating { get; set; }

        [JsonProperty("csaRating")]
        public string CsaRating { get; set; }

        [JsonProperty("cscfRating")]
        public string CscfRating { get; set; }

        [JsonProperty("czfilmRating")]
        public string CzfilmRating { get; set; }

        [JsonProperty("djctqRating")]
        public string DjctqRating { get; set; }

        [JsonProperty("djctqRatingReasons")]
        public List<string> DjctqRatingReasons { get; set; }

        [JsonProperty("ecbmctRating")]
        public string EcbmctRating { get; set; }

        [JsonProperty("eefilmRating")]
        public string EefilmRating { get; set; }

        [JsonProperty("egfilmRating")]
        public string EgfilmRating { get; set; }

        [JsonProperty("eirinRating")]
        public string EirinRating { get; set; }

        [JsonProperty("fcbmRating")]
        public string FcbmRating { get; set; }

        [JsonProperty("fcoRating")]
        public string FcoRating { get; set; }

        [JsonProperty("fmocRating")]
        public string FmocRating { get; set; }

        [JsonProperty("fpbRating")]
        public string FpbRating { get; set; }

        [JsonProperty("fpbRatingReasons")]
        public List<string> FpbRatingReasons { get; set; }

        [JsonProperty("fskRating")]
        public string FskRating { get; set; }

        [JsonProperty("grfilmRating")]
        public string GrfilmRating { get; set; }

        [JsonProperty("icaaRating")]
        public string IcaaRating { get; set; }

        [JsonProperty("ifcoRating")]
        public string IfcoRating { get; set; }

        [JsonProperty("ilfilmRating")]
        public string IlfilmRating { get; set; }

        [JsonProperty("incaaRating")]
        public string IncaaRating { get; set; }

        [JsonProperty("kfcbRating")]
        public string KfcbRating { get; set; }

        [JsonProperty("kijkwijzerRating")]
        public string KijkwijzerRating { get; set; }

        [JsonProperty("kmrbRating")]
        public string KmrbRating { get; set; }

        [JsonProperty("lsfRating")]
        public string LsfRating { get; set; }

        [JsonProperty("mccaaRating")]
        public string MccaaRating { get; set; }

        [JsonProperty("mccypRating")]
        public string MccypRating { get; set; }

        [JsonProperty("mcstRating")]
        public string McstRating { get; set; }

        [JsonProperty("mdaRating")]
        public string MdaRating { get; set; }

        [JsonProperty("medietilsynetRating")]
        public string MedietilsynetRating { get; set; }

        [JsonProperty("mekuRating")]
        public string MekuRating { get; set; }

        [JsonProperty("mibacRating")]
        public string MibacRating { get; set; }

        [JsonProperty("mocRating")]
        public string MocRating { get; set; }

        [JsonProperty("moctwRating")]
        public string MoctwRating { get; set; }

        [JsonProperty("mpaaRating")]
        public string MpaaRating { get; set; }

        [JsonProperty("mpaatRating")]
        public string MpaatRating { get; set; }

        [JsonProperty("mtrcbRating")]
        public string MtrcbRating { get; set; }

        [JsonProperty("nbcRating")]
        public string NbcRating { get; set; }

        [JsonProperty("nbcplRating")]
        public string NbcplRating { get; set; }

        [JsonProperty("nfrcRating")]
        public string NfrcRating { get; set; }

        [JsonProperty("nfvcbRating")]
        public string NfvcbRating { get; set; }

        [JsonProperty("nkclvRating")]
        public string NkclvRating { get; set; }

        [JsonProperty("oflcRating")]
        public string OflcRating { get; set; }

        [JsonProperty("pefilmRating")]
        public string PefilmRating { get; set; }

        [JsonProperty("rcnofRating")]
        public string RcnofRating { get; set; }

        [JsonProperty("resorteviolenciaRating")]
        public string ResorteviolenciaRating { get; set; }

        [JsonProperty("rtcRating")]
        public string RtcRating { get; set; }

        [JsonProperty("rteRating")]
        public string RteRating { get; set; }

        [JsonProperty("russiaRating")]
        public string RussiaRating { get; set; }

        [JsonProperty("skfilmRating")]
        public string SkfilmRating { get; set; }

        [JsonProperty("smaisRating")]
        public string SmaisRating { get; set; }

        [JsonProperty("smsaRating")]
        public string SmsaRating { get; set; }

        [JsonProperty("tvpgRating")]
        public string TvpgRating { get; set; }

        [JsonProperty("ytRating")]
        public string YtRating { get; set; }
    }

    public class RegionRestriction
    {
        [JsonProperty("allowed")]
        public List<string> Allowed { get; set; }

        [JsonProperty("blocked")]
        public List<string> Blocked { get; set; }
    }

    public class FileDetails
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileSize")]
        public long FileSize { get; set; }

        [JsonProperty("fileType")]
        public string FileType { get; set; }

        [JsonProperty("container")]
        public string Container { get; set; }

        [JsonProperty("videoStreams")]
        public List<VideoStream> VideoStreams { get; set; }

        [JsonProperty("audioStreams")]
        public List<AudioStream> AudioStreams { get; set; }

        [JsonProperty("durationMs")]
        public long DurationMs { get; set; }

        [JsonProperty("bitrateBps")]
        public long BitrateBps { get; set; }

        [JsonProperty("creationTime")]
        public string CreationTime { get; set; }
    }

    public class AudioStream
    {
        [JsonProperty("channelCount")]
        public long ChannelCount { get; set; }

        [JsonProperty("codec")]
        public string Codec { get; set; }

        [JsonProperty("bitrateBps")]
        public long BitrateBps { get; set; }

        [JsonProperty("vendor")]
        public string Vendor { get; set; }
    }

    public class VideoStream
    {
        [JsonProperty("widthPixels")]
        public long WidthPixels { get; set; }

        [JsonProperty("heightPixels")]
        public long HeightPixels { get; set; }

        [JsonProperty("frameRateFps")]
        public double FrameRateFps { get; set; }

        [JsonProperty("aspectRatio")]
        public double AspectRatio { get; set; }

        [JsonProperty("codec")]
        public string Codec { get; set; }

        [JsonProperty("bitrateBps")]
        public long BitrateBps { get; set; }

        [JsonProperty("rotation")]
        public string Rotation { get; set; }

        [JsonProperty("vendor")]
        public string Vendor { get; set; }
    }

    public class LiveStreamingDetails
    {
        [JsonProperty("actualStartTime")]
        public DateTimeOffset ActualStartTime { get; set; }

        [JsonProperty("actualEndTime")]
        public DateTimeOffset ActualEndTime { get; set; }

        [JsonProperty("scheduledStartTime")]
        public DateTimeOffset ScheduledStartTime { get; set; }

        [JsonProperty("scheduledEndTime")]
        public DateTimeOffset ScheduledEndTime { get; set; }

        [JsonProperty("concurrentViewers")]
        public long ConcurrentViewers { get; set; }

        [JsonProperty("activeLiveChatId")]
        public string ActiveLiveChatId { get; set; }
    }

    public class Localization
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class Player
    {
        [JsonProperty("embedHtml")]
        public string EmbedHtml { get; set; }

        [JsonProperty("embedHeight")]
        public long EmbedHeight { get; set; }

        [JsonProperty("embedWidth")]
        public long EmbedWidth { get; set; }
    }

    public class ProcessingDetails
    {
        [JsonProperty("processingStatus")]
        public string ProcessingStatus { get; set; }

        [JsonProperty("processingProgress")]
        public ProcessingProgress ProcessingProgress { get; set; }

        [JsonProperty("processingFailureReason")]
        public string ProcessingFailureReason { get; set; }

        [JsonProperty("fileDetailsAvailability")]
        public string FileDetailsAvailability { get; set; }

        [JsonProperty("processingIssuesAvailability")]
        public string ProcessingIssuesAvailability { get; set; }

        [JsonProperty("tagSuggestionsAvailability")]
        public string TagSuggestionsAvailability { get; set; }

        [JsonProperty("editorSuggestionsAvailability")]
        public string EditorSuggestionsAvailability { get; set; }

        [JsonProperty("thumbnailsAvailability")]
        public string ThumbnailsAvailability { get; set; }
    }

    public class ProcessingProgress
    {
        [JsonProperty("partsTotal")]
        public long PartsTotal { get; set; }

        [JsonProperty("partsProcessed")]
        public long PartsProcessed { get; set; }

        [JsonProperty("timeLeftMs")]
        public long TimeLeftMs { get; set; }
    }

    public class RecordingDetails
    {
        [JsonProperty("recordingDate")]
        public DateTimeOffset RecordingDate { get; set; }
    }

    public class Snippet
    {
        [JsonProperty("publishedAt")]
        public DateTimeOffset PublishedAt { get; set; }

        [JsonProperty("channelId")]
        public string ChannelId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("thumbnails")]
        public Dictionary<string, Thumbnail> Thumbnails { get; set; }

        [JsonProperty("channelTitle")]
        public string ChannelTitle { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }

        [JsonProperty("liveBroadcastContent")]
        public string LiveBroadcastContent { get; set; }

        [JsonProperty("defaultLanguage")]
        public string DefaultLanguage { get; set; }

        [JsonProperty("localized")]
        public Localization Localized { get; set; }

        [JsonProperty("defaultAudioLanguage")]
        public string DefaultAudioLanguage { get; set; }
    }

    public class Thumbnail
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }
    }

    public class Statistics
    {
        [JsonProperty("viewCount")]
        public long ViewCount { get; set; }

        [JsonProperty("likeCount")]
        public long LikeCount { get; set; }

        [JsonProperty("dislikeCount")]
        public long DislikeCount { get; set; }

        [JsonProperty("favoriteCount")]
        public long FavoriteCount { get; set; }

        [JsonProperty("commentCount")]
        public long CommentCount { get; set; }
    }

    public class Status
    {
        [JsonProperty("uploadStatus")]
        public string UploadStatus { get; set; }

        [JsonProperty("failureReason")]
        public string FailureReason { get; set; }

        [JsonProperty("rejectionReason")]
        public string RejectionReason { get; set; }

        [JsonProperty("privacyStatus")]
        public string PrivacyStatus { get; set; }

        [JsonProperty("publishAt")]
        public DateTimeOffset PublishAt { get; set; }

        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("embeddable")]
        public bool Embeddable { get; set; }

        [JsonProperty("publicStatsViewable")]
        public bool PublicStatsViewable { get; set; }
    }

    public class Suggestions
    {
        [JsonProperty("processingErrors")]
        public List<string> ProcessingErrors { get; set; }

        [JsonProperty("processingWarnings")]
        public List<string> ProcessingWarnings { get; set; }

        [JsonProperty("processingHints")]
        public List<string> ProcessingHints { get; set; }

        [JsonProperty("tagSuggestions")]
        public List<TagSuggestion> TagSuggestions { get; set; }

        [JsonProperty("editorSuggestions")]
        public List<string> EditorSuggestions { get; set; }
    }

    public class TagSuggestion
    {
        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("categoryRestricts")]
        public List<string> CategoryRestricts { get; set; }
    }

    public class TopicDetails
    {
        [JsonProperty("topicIds")]
        public List<string> TopicIds { get; set; }

        [JsonProperty("relevantTopicIds")]
        public List<string> RelevantTopicIds { get; set; }

        [JsonProperty("topicCategories")]
        public List<string> TopicCategories { get; set; }
    }
}
