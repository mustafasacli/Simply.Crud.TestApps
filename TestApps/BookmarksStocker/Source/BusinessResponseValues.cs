namespace BookmarksStocker.Source
{
    /// <summary>
    /// Defines the <see cref="BusinessResponseValues" />
    /// </summary>
    public sealed class BusinessResponseValues
    {
        /// <summary>
        /// Defines the NullIdValue
        /// Value: -100.
        /// </summary>
        public static readonly int NullIdValue = -100;

        /// <summary>
        /// Defines the NullEntityValue
        /// Value: -101.
        /// </summary>
        public static readonly int NullEntityValue = -101;

        /// <summary>
        /// Defines the NotPositiveIdValue
        /// Value: -102.
        /// </summary>
        public static readonly int NotPositiveIdValue = -102;

        /// <summary>
        /// Defines the NullViewModelValue
        /// Value: -103.
        /// </summary>
        public static readonly int NullViewModelValue = -103;

        /// <summary>
        /// Defines the EmtyIdValue
        /// Value: -104.
        /// </summary>
        public static readonly int EmptyIdValue = -104;

        /// <summary>
        /// Defines the InvalidTcNo
        /// Value: -105.
        /// </summary>
        public static readonly int GecersizTcNo = -105;

        /// <summary>
        /// Defines the Invalid Yurt Id
        /// Value: -106.
        /// </summary>
        public static readonly int GecersizYurtId = -106;

        /// <summary>
        /// Defines the ValidationErrorResult
        /// Value: -200.
        /// </summary>
        public static readonly int ValidationErrorResult = -200;

        /// <summary>
        /// Kullanıcı, bu başvurunun sahibi değilse dönecek hata kodudur.
        /// Value: -300.
        /// </summary>
        public static readonly int BasvuruVeKulaniciUyusmuyor = -300;

        /// <summary>
        /// Kullanıcı, bu yurdun kurucusu değilse dönecek hata kodudur.
        /// Value: -400.
        /// </summary>
        public static readonly int YurtVeKulaniciUyusmuyor = -400;

        /// <summary>
        /// Eklenecek Ip kullanılıyor ise dönecek hata kodudur.
        /// Value: -401.
        /// </summary>
        public static readonly int IpZatenKullaniliyor = -401;

        /// <summary>
        /// Yurt ve Talep uyuşmuyor.
        /// value: -402..
        /// </summary>
        public static readonly int YurtTalepUyusmuyor = -402;

        /// <summary>
        /// Defines the InternalError.
        /// Value: -500.
        /// </summary>
        public static readonly int InternalError = -500;

        /// <summary>
        /// Defines the InternalFileDeleteError.
        /// Value: -600.
        /// </summary>
        public static readonly int InternalFileDeleteError = -600;

        /// <summary>
        /// Defines the TakipDogrulanmis.
        /// Value: -601.
        /// </summary>
        public static readonly int TakipZatenDogrulanmis = -601;

        /// <summary>
        /// Defines the ServisDogrulamaTamamlanmadi.
        /// Value: -602.
        /// </summary>
        public static readonly int ServisDogrulamaTamamlanmadi = -602;

        /// <summary>
        /// Defines the BosServisSonucu.
        /// Value: -603.
        /// </summary>
        public static readonly int BosServisSonucu = -603;

        /// <summary>
        /// Defines the BosServisSonucu.
        /// Value: -604.
        /// </summary>
        public static readonly int GecersizServisSonucu = -604;
    }
}