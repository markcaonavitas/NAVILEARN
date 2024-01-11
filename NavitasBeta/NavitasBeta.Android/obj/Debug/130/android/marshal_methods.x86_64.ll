; ModuleID = 'obj\Debug\130\android\marshal_methods.x86_64.ll'
source_filename = "obj\Debug\130\android\marshal_methods.x86_64.ll"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.MonoClass = type opaque

%struct.MarshalMethodsManagedClass = type {
	i32,; uint32_t token
	%struct.MonoClass*; MonoClass* klass
}

%struct.MarshalMethodName = type {
	i64,; uint64_t id
	i8*; char* name
}

%class._JNIEnv = type opaque

%class._jobject = type {
	i8; uint8_t b
}

%class._jclass = type {
	i8; uint8_t b
}

%class._jstring = type {
	i8; uint8_t b
}

%class._jthrowable = type {
	i8; uint8_t b
}

%class._jarray = type {
	i8; uint8_t b
}

%class._jobjectArray = type {
	i8; uint8_t b
}

%class._jbooleanArray = type {
	i8; uint8_t b
}

%class._jbyteArray = type {
	i8; uint8_t b
}

%class._jcharArray = type {
	i8; uint8_t b
}

%class._jshortArray = type {
	i8; uint8_t b
}

%class._jintArray = type {
	i8; uint8_t b
}

%class._jlongArray = type {
	i8; uint8_t b
}

%class._jfloatArray = type {
	i8; uint8_t b
}

%class._jdoubleArray = type {
	i8; uint8_t b
}

; assembly_image_cache
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 8
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [362 x i64] [
	i64 24362543149721218, ; 0: Xamarin.AndroidX.DynamicAnimation => 0x568d9a9a43a682 => 96
	i64 45886493525149769, ; 1: Xamarin.Forms.Material => 0xa30585d28e0849 => 140
	i64 120698629574877762, ; 2: Mono.Android => 0x1accec39cafe242 => 12
	i64 210515253464952879, ; 3: Xamarin.AndroidX.Collection.dll => 0x2ebe681f694702f => 85
	i64 229794953483747371, ; 4: System.ValueTuple.dll => 0x330654aed93802b => 3
	i64 232391251801502327, ; 5: Xamarin.AndroidX.SavedState.dll => 0x3399e9cbc897277 => 125
	i64 295915112840604065, ; 6: Xamarin.AndroidX.SlidingPaneLayout => 0x41b4d3a3088a9a1 => 126
	i64 316157742385208084, ; 7: Xamarin.AndroidX.Core.Core.Ktx.dll => 0x46337caa7dc1b14 => 90
	i64 403694196094993479, ; 8: Xamarin.AndroidX.MediaRouter => 0x59a35bb84210447 => 114
	i64 562355463320435893, ; 9: Syncfusion.SfGauge.XForms.dll => 0x7cde3507cb6fcb5 => 26
	i64 590536689428908136, ; 10: Xamarin.Android.Arch.Lifecycle.ViewModel.dll => 0x83201fd803ec868 => 42
	i64 627200827541438871, ; 11: OxyPlot.Xamarin.Forms.Platform.Android => 0x8b443d460704197 => 18
	i64 634308326490598313, ; 12: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x8cd840fee8b6ba9 => 108
	i64 687654259221141486, ; 13: Xamarin.GooglePlayServices.Base => 0x98b09e7c92917ee => 146
	i64 702024105029695270, ; 14: System.Drawing.Common => 0x9be17343c0e7726 => 159
	i64 720058930071658100, ; 15: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x9fe29c82844de74 => 101
	i64 799765834175365804, ; 16: System.ComponentModel.dll => 0xb1956c9f18442ac => 5
	i64 816102801403336439, ; 17: Xamarin.Android.Support.Collections => 0xb53612c89d8d6f7 => 46
	i64 846634227898301275, ; 18: Xamarin.Android.Arch.Lifecycle.LiveData.Core => 0xbbfd9583890bb5b => 39
	i64 872800313462103108, ; 19: Xamarin.AndroidX.DrawerLayout => 0xc1ccf42c3c21c44 => 95
	i64 940822596282819491, ; 20: System.Transactions => 0xd0e792aa81923a3 => 174
	i64 996343623809489702, ; 21: Xamarin.Forms.Platform => 0xdd3b93f3b63db26 => 142
	i64 1000557547492888992, ; 22: Mono.Security.dll => 0xde2b1c9cba651a0 => 180
	i64 1120440138749646132, ; 23: Xamarin.Google.Android.Material.dll => 0xf8c9a5eae431534 => 144
	i64 1315114680217950157, ; 24: Xamarin.AndroidX.Arch.Core.Common.dll => 0x124039d5794ad7cd => 80
	i64 1342439039765371018, ; 25: Xamarin.Android.Support.Fragment => 0x12a14d31b1d4d88a => 57
	i64 1425944114962822056, ; 26: System.Runtime.Serialization.dll => 0x13c9f89e19eaf3a8 => 9
	i64 1490981186906623721, ; 27: Xamarin.Android.Support.VersionedParcelable => 0x14b1077d6c5c0ee9 => 73
	i64 1624659445732251991, ; 28: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0x168bf32877da9957 => 78
	i64 1628611045998245443, ; 29: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0x1699fd1e1a00b643 => 110
	i64 1636321030536304333, ; 30: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0x16b5614ec39e16cd => 102
	i64 1731380447121279447, ; 31: Newtonsoft.Json => 0x18071957e9b889d7 => 16
	i64 1744702963312407042, ; 32: Xamarin.Android.Support.v7.AppCompat => 0x18366e19eeceb202 => 67
	i64 1795316252682057001, ; 33: Xamarin.AndroidX.AppCompat.dll => 0x18ea3e9eac997529 => 79
	i64 1836611346387731153, ; 34: Xamarin.AndroidX.SavedState => 0x197cf449ebe482d1 => 125
	i64 1860886102525309849, ; 35: Xamarin.Android.Support.v7.RecyclerView.dll => 0x19d3320d047b7399 => 71
	i64 1875917498431009007, ; 36: Xamarin.AndroidX.Annotation.dll => 0x1a08990699eb70ef => 76
	i64 1938067011858688285, ; 37: Xamarin.Android.Support.v4.dll => 0x1ae565add0bd691d => 66
	i64 1981742497975770890, ; 38: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x1b80904d5c241f0a => 109
	i64 2064708342624596306, ; 39: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x1ca7514c5eecb152 => 153
	i64 2076847052340733488, ; 40: Syncfusion.Core.XForms => 0x1cd27163f7962630 => 22
	i64 2133195048986300728, ; 41: Newtonsoft.Json.dll => 0x1d9aa1984b735138 => 16
	i64 2136356949452311481, ; 42: Xamarin.AndroidX.MultiDex.dll => 0x1da5dd539d8acbb9 => 115
	i64 2165725771938924357, ; 43: Xamarin.AndroidX.Browser => 0x1e0e341d75540745 => 83
	i64 2262844636196693701, ; 44: Xamarin.AndroidX.DrawerLayout.dll => 0x1f673d352266e6c5 => 95
	i64 2284400282711631002, ; 45: System.Web.Services => 0x1fb3d1f42fd4249a => 178
	i64 2304837677853103545, ; 46: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0x1ffc6da80d5ed5b9 => 124
	i64 2329709569556905518, ; 47: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x2054ca829b447e2e => 105
	i64 2469392061734276978, ; 48: Syncfusion.Core.XForms.Android.dll => 0x22450aff2ad74f72 => 21
	i64 2470498323731680442, ; 49: Xamarin.AndroidX.CoordinatorLayout => 0x2248f922dc398cba => 89
	i64 2479423007379663237, ; 50: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x2268ae16b2cba985 => 131
	i64 2497223385847772520, ; 51: System.Runtime => 0x22a7eb7046413568 => 33
	i64 2547086958574651984, ; 52: Xamarin.AndroidX.Activity.dll => 0x2359121801df4a50 => 75
	i64 2592350477072141967, ; 53: System.Xml.dll => 0x23f9e10627330e8f => 34
	i64 2624866290265602282, ; 54: mscorlib.dll => 0x246d65fbde2db8ea => 14
	i64 2694427813909235223, ; 55: Xamarin.AndroidX.Preference.dll => 0x256487d230fe0617 => 120
	i64 2787234703088983483, ; 56: Xamarin.AndroidX.Startup.StartupRuntime => 0x26ae3f31ef429dbb => 127
	i64 2949706848458024531, ; 57: Xamarin.Android.Support.SlidingPaneLayout => 0x28ef76c01de0a653 => 63
	i64 2960931600190307745, ; 58: Xamarin.Forms.Core => 0x2917579a49927da1 => 137
	i64 2977248461349026546, ; 59: Xamarin.Android.Support.DrawerLayout => 0x29514fb392c97af2 => 56
	i64 3017704767998173186, ; 60: Xamarin.Google.Android.Material => 0x29e10a7f7d88a002 => 144
	i64 3022227708164871115, ; 61: Xamarin.Android.Support.Media.Compat.dll => 0x29f11c168f8293cb => 61
	i64 3289520064315143713, ; 62: Xamarin.AndroidX.Lifecycle.Common => 0x2da6b911e3063621 => 104
	i64 3303437397778967116, ; 63: Xamarin.AndroidX.Annotation.Experimental => 0x2dd82acf985b2a4c => 77
	i64 3311221304742556517, ; 64: System.Numerics.Vectors.dll => 0x2df3d23ba9e2b365 => 32
	i64 3344514922410554693, ; 65: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x2e6a1a9a18463545 => 156
	i64 3411255996856937470, ; 66: Xamarin.GooglePlayServices.Basement => 0x2f5737416a942bfe => 147
	i64 3493805808809882663, ; 67: Xamarin.AndroidX.Tracing.Tracing.dll => 0x307c7ddf444f3427 => 129
	i64 3522470458906976663, ; 68: Xamarin.AndroidX.SwipeRefreshLayout => 0x30e2543832f52197 => 128
	i64 3531994851595924923, ; 69: System.Numerics => 0x31042a9aade235bb => 31
	i64 3571415421602489686, ; 70: System.Runtime.dll => 0x319037675df7e556 => 33
	i64 3647754201059316852, ; 71: System.Xml.ReaderWriter => 0x329f6d1e86145474 => 171
	i64 3716579019761409177, ; 72: netstandard.dll => 0x3393f0ed5c8c5c99 => 172
	i64 3727469159507183293, ; 73: Xamarin.AndroidX.RecyclerView => 0x33baa1739ba646bd => 123
	i64 3772598417116884899, ; 74: Xamarin.AndroidX.DynamicAnimation.dll => 0x345af645b473efa3 => 96
	i64 3869649043256705283, ; 75: System.Diagnostics.Tools => 0x35b3c14d74bf0103 => 2
	i64 4154383907710350974, ; 76: System.ComponentModel => 0x39a7562737acb67e => 5
	i64 4201423742386704971, ; 77: Xamarin.AndroidX.Core.Core.Ktx => 0x3a4e74a233da124b => 90
	i64 4204580399898516257, ; 78: Syncfusion.SfGauge.XForms.Android => 0x3a59ab98cd855b21 => 25
	i64 4247996603072512073, ; 79: Xamarin.GooglePlayServices.Tasks => 0x3af3ea6755340049 => 149
	i64 4252163538099460320, ; 80: Xamarin.Android.Support.ViewPager.dll => 0x3b02b8357f4958e0 => 74
	i64 4264996749430196783, ; 81: Xamarin.Android.Support.Transition.dll => 0x3b304ff259fb8a2f => 65
	i64 4349341163892612332, ; 82: Xamarin.Android.Support.DocumentFile => 0x3c5bf6bea8cd9cec => 55
	i64 4416987920449902723, ; 83: Xamarin.Android.Support.AsyncLayoutInflater.dll => 0x3d4c4b1c879b9883 => 45
	i64 4525561845656915374, ; 84: System.ServiceModel.Internals => 0x3ece06856b710dae => 158
	i64 4636684751163556186, ; 85: Xamarin.AndroidX.VersionedParcelable.dll => 0x4058d0370893015a => 133
	i64 4716677666592453464, ; 86: System.Xml.XmlSerializer => 0x417501590542f358 => 166
	i64 4759461199762736555, ; 87: Xamarin.AndroidX.Lifecycle.Process.dll => 0x420d00be961cc5ab => 107
	i64 4782108999019072045, ; 88: Xamarin.AndroidX.AsyncLayoutInflater.dll => 0x425d76cc43bb0a2d => 82
	i64 4794310189461587505, ; 89: Xamarin.AndroidX.Activity => 0x4288cfb749e4c631 => 75
	i64 4795410492532947900, ; 90: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0x428cb86f8f9b7bbc => 128
	i64 4841234827713643511, ; 91: Xamarin.Android.Support.CoordinaterLayout => 0x432f856d041f03f7 => 48
	i64 4963205065368577818, ; 92: Xamarin.Android.Support.LocalBroadcastManager.dll => 0x44e0d8b5f4b6a71a => 60
	i64 5081566143765835342, ; 93: System.Resources.ResourceManager.dll => 0x4685597c05d06e4e => 1
	i64 5099468265966638712, ; 94: System.Resources.ResourceManager => 0x46c4f35ea8519678 => 1
	i64 5142919913060024034, ; 95: Xamarin.Forms.Platform.Android.dll => 0x475f52699e39bee2 => 141
	i64 5178572682164047940, ; 96: Xamarin.Android.Support.Print.dll => 0x47ddfc6acbee1044 => 62
	i64 5203618020066742981, ; 97: Xamarin.Essentials => 0x4836f704f0e652c5 => 136
	i64 5205316157927637098, ; 98: Xamarin.AndroidX.LocalBroadcastManager => 0x483cff7778e0c06a => 112
	i64 5256995213548036366, ; 99: Xamarin.Forms.Maps.Android.dll => 0x48f4994b4175a10e => 138
	i64 5286637047618374099, ; 100: OxyPlot => 0x495de8628fb689d3 => 17
	i64 5288341611614403055, ; 101: Xamarin.Android.Support.Interpolator.dll => 0x4963f6ad4b3179ef => 58
	i64 5348796042099802469, ; 102: Xamarin.AndroidX.Media => 0x4a3abda9415fc165 => 113
	i64 5376510917114486089, ; 103: Xamarin.AndroidX.VectorDrawable.Animated => 0x4a9d3431719e5d49 => 131
	i64 5408338804355907810, ; 104: Xamarin.AndroidX.Transition => 0x4b0e477cea9840e2 => 130
	i64 5439315836349573567, ; 105: Xamarin.Android.Support.Animated.Vector.Drawable.dll => 0x4b7c54ef36c5e9bf => 43
	i64 5446034149219586269, ; 106: System.Diagnostics.Debug => 0x4b94333452e150dd => 157
	i64 5451019430259338467, ; 107: Xamarin.AndroidX.ConstraintLayout.dll => 0x4ba5e94a845c2ce3 => 88
	i64 5507995362134886206, ; 108: System.Core.dll => 0x4c705499688c873e => 27
	i64 5563049671862343767, ; 109: Xamarin.AndroidX.Palette.dll => 0x4d33ec33c7355857 => 119
	i64 5692067934154308417, ; 110: Xamarin.AndroidX.ViewPager2.dll => 0x4efe49a0d4a8bb41 => 135
	i64 5757522595884336624, ; 111: Xamarin.AndroidX.Concurrent.Futures.dll => 0x4fe6d44bd9f885f0 => 86
	i64 5767696078500135884, ; 112: Xamarin.Android.Support.Annotations.dll => 0x500af9065b6a03cc => 44
	i64 5814345312393086621, ; 113: Xamarin.AndroidX.Preference => 0x50b0b44182a5c69d => 120
	i64 5896680224035167651, ; 114: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x51d5376bfbafdda3 => 106
	i64 6044705416426755068, ; 115: Xamarin.Android.Support.SwipeRefreshLayout.dll => 0x53e31b8ccdff13fc => 64
	i64 6085203216496545422, ; 116: Xamarin.Forms.Platform.dll => 0x5472fc15a9574e8e => 142
	i64 6086316965293125504, ; 117: FormsViewGroup.dll => 0x5476f10882baef80 => 10
	i64 6311200438583329442, ; 118: Xamarin.Android.Support.LocalBroadcastManager => 0x5795e35c580c82a2 => 60
	i64 6319713645133255417, ; 119: Xamarin.AndroidX.Lifecycle.Runtime => 0x57b42213b45b52f9 => 108
	i64 6401687960814735282, ; 120: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0x58d75d486341cfb2 => 105
	i64 6405879832841858445, ; 121: Xamarin.Android.Support.Vector.Drawable.dll => 0x58e641c4a660ad8d => 72
	i64 6437453774371681866, ; 122: Xamarin.Android.Support.v7.Palette => 0x59566e19c76bf64a => 70
	i64 6504860066809920875, ; 123: Xamarin.AndroidX.Browser.dll => 0x5a45e7c43bd43d6b => 83
	i64 6548213210057960872, ; 124: Xamarin.AndroidX.CustomView.dll => 0x5adfed387b066da8 => 93
	i64 6588599331800941662, ; 125: Xamarin.Android.Support.v4 => 0x5b6f682f335f045e => 66
	i64 6591024623626361694, ; 126: System.Web.Services.dll => 0x5b7805f9751a1b5e => 178
	i64 6659513131007730089, ; 127: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0x5c6b57e8b6c3e1a9 => 101
	i64 6662274349274916432, ; 128: Syncfusion.SfGauge.Android.dll => 0x5c752738f0ba6650 => 24
	i64 6876862101832370452, ; 129: System.Xml.Linq => 0x5f6f85a57d108914 => 35
	i64 6894844156784520562, ; 130: System.Numerics.Vectors => 0x5faf683aead1ad72 => 32
	i64 6929694340031562077, ; 131: Syncfusion.SfGauge.XForms => 0x602b3849839dc15d => 26
	i64 7026608356547306326, ; 132: Syncfusion.Core.XForms.dll => 0x618387125bcb2356 => 22
	i64 7036436454368433159, ; 133: Xamarin.AndroidX.Legacy.Support.V4.dll => 0x61a671acb33d5407 => 103
	i64 7095498839588419757, ; 134: NavitasBeta => 0x62784699dddd38ad => 15
	i64 7103753931438454322, ; 135: Xamarin.AndroidX.Interpolator.dll => 0x62959a90372c7632 => 100
	i64 7141281584637745974, ; 136: Xamarin.GooglePlayServices.Maps.dll => 0x631aedc3dd5f1b36 => 148
	i64 7164916624638427275, ; 137: Xamarin.Android.Support.v7.MediaRouter.dll => 0x636ee5b570dd408b => 69
	i64 7194160955514091247, ; 138: Xamarin.Android.Support.CursorAdapter.dll => 0x63d6cb45d266f6ef => 51
	i64 7270811800166795866, ; 139: System.Linq => 0x64e71ccf51a90a5a => 170
	i64 7338192458477945005, ; 140: System.Reflection => 0x65d67f295d0740ad => 163
	i64 7488575175965059935, ; 141: System.Xml.Linq.dll => 0x67ecc3724534ab5f => 35
	i64 7489048572193775167, ; 142: System.ObjectModel => 0x67ee71ff6b419e3f => 168
	i64 7515774541925068790, ; 143: ParseManager.Droid => 0x684d6520275e2ff6 => 20
	i64 7635363394907363464, ; 144: Xamarin.Forms.Core.dll => 0x69f6428dc4795888 => 137
	i64 7637365915383206639, ; 145: Xamarin.Essentials.dll => 0x69fd5fd5e61792ef => 136
	i64 7654504624184590948, ; 146: System.Net.Http => 0x6a3a4366801b8264 => 30
	i64 7735352534559001595, ; 147: Xamarin.Kotlin.StdLib.dll => 0x6b597e2582ce8bfb => 152
	i64 7820441508502274321, ; 148: System.Data => 0x6c87ca1e14ff8111 => 173
	i64 7821246742157274664, ; 149: Xamarin.Android.Support.AsyncLayoutInflater => 0x6c8aa67926f72e28 => 45
	i64 7836164640616011524, ; 150: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x6cbfa6390d64d704 => 78
	i64 7879037620440914030, ; 151: Xamarin.Android.Support.v7.AppCompat.dll => 0x6d57f6f88a51d86e => 67
	i64 8003488281596490781, ; 152: Xamarin.Android.Support.v7.MediaRouter => 0x6f121a30148f741d => 69
	i64 8044118961405839122, ; 153: System.ComponentModel.Composition => 0x6fa2739369944712 => 177
	i64 8064050204834738623, ; 154: System.Collections.dll => 0x6fe942efa61731bf => 162
	i64 8083354569033831015, ; 155: Xamarin.AndroidX.Lifecycle.Common.dll => 0x702dd82730cad267 => 104
	i64 8101777744205214367, ; 156: Xamarin.Android.Support.Annotations => 0x706f4beeec84729f => 44
	i64 8103644804370223335, ; 157: System.Data.DataSetExtensions.dll => 0x7075ee03be6d50e7 => 175
	i64 8113615946733131500, ; 158: System.Reflection.Extensions => 0x70995ab73cf916ec => 6
	i64 8167236081217502503, ; 159: Java.Interop.dll => 0x7157d9f1a9b8fd27 => 11
	i64 8185542183669246576, ; 160: System.Collections => 0x7198e33f4794aa70 => 162
	i64 8187640529827139739, ; 161: Xamarin.KotlinX.Coroutines.Android => 0x71a057ae90f0109b => 155
	i64 8196541262927413903, ; 162: Xamarin.Android.Support.Interpolator => 0x71bff6d9fb9ec28f => 58
	i64 8290740647658429042, ; 163: System.Runtime.Extensions => 0x730ea0b15c929a72 => 169
	i64 8377847505162989171, ; 164: OxyPlot.Xamarin.Forms => 0x744417eb0fa1ee73 => 4
	i64 8385935383968044654, ; 165: Xamarin.Android.Arch.Lifecycle.Runtime.dll => 0x7460d3cd16cb566e => 41
	i64 8398329775253868912, ; 166: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x748cdc6f3097d170 => 87
	i64 8400357532724379117, ; 167: Xamarin.AndroidX.Navigation.UI.dll => 0x749410ab44503ded => 118
	i64 8426919725312979251, ; 168: Xamarin.AndroidX.Lifecycle.Process => 0x74f26ed7aa033133 => 107
	i64 8459685143463783256, ; 169: NavitasBeta.Android => 0x7566d6d10fceeb58 => 0
	i64 8598790081731763592, ; 170: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x77550a055fc61d88 => 98
	i64 8601935802264776013, ; 171: Xamarin.AndroidX.Transition.dll => 0x7760370982b4ed4d => 130
	i64 8626175481042262068, ; 172: Java.Interop => 0x77b654e585b55834 => 11
	i64 8639588376636138208, ; 173: Xamarin.AndroidX.Navigation.Runtime => 0x77e5fbdaa2fda2e0 => 117
	i64 8684531736582871431, ; 174: System.IO.Compression.FileSystem => 0x7885a79a0fa0d987 => 176
	i64 8796457469147618732, ; 175: Xamarin.Android.Support.CustomTabs => 0x7a134b766a601dac => 52
	i64 8808820144457481518, ; 176: Xamarin.Android.Support.Loader.dll => 0x7a3f374010b17d2e => 59
	i64 8853378295825400934, ; 177: Xamarin.Kotlin.StdLib.Common.dll => 0x7add84a720d38466 => 151
	i64 8917102979740339192, ; 178: Xamarin.Android.Support.DocumentFile.dll => 0x7bbfe9ea4d000bf8 => 55
	i64 8951477988056063522, ; 179: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0x7c3a09cd9ccf5e22 => 122
	i64 8974768617058262486, ; 180: Xamarin.AndroidX.MediaRouter.dll => 0x7c8cc881c114ddd6 => 114
	i64 9083778504339266700, ; 181: OxyPlot.Xamarin.Android.dll => 0x7e10106bf9789c8c => 8
	i64 9290408134796603763, ; 182: Xamarin.Forms.Material.dll => 0x80ee28f9d4f37173 => 140
	i64 9312692141327339315, ; 183: Xamarin.AndroidX.ViewPager2 => 0x813d54296a634f33 => 135
	i64 9324707631942237306, ; 184: Xamarin.AndroidX.AppCompat => 0x8168042fd44a7c7a => 79
	i64 9475595603812259686, ; 185: Xamarin.Android.Support.Design => 0x838013ff707b9766 => 54
	i64 9584643793929893533, ; 186: System.IO.dll => 0x85037ebfbbd7f69d => 165
	i64 9659729154652888475, ; 187: System.Text.RegularExpressions => 0x860e407c9991dd9b => 161
	i64 9662334977499516867, ; 188: System.Numerics.dll => 0x8617827802b0cfc3 => 31
	i64 9678050649315576968, ; 189: Xamarin.AndroidX.CoordinatorLayout.dll => 0x864f57c9feb18c88 => 89
	i64 9711637524876806384, ; 190: Xamarin.AndroidX.Media.dll => 0x86c6aadfd9a2c8f0 => 113
	i64 9808709177481450983, ; 191: Mono.Android.dll => 0x881f890734e555e7 => 12
	i64 9825649861376906464, ; 192: Xamarin.AndroidX.Concurrent.Futures => 0x885bb87d8abc94e0 => 86
	i64 9834056768316610435, ; 193: System.Transactions.dll => 0x8879968718899783 => 174
	i64 9866412715007501892, ; 194: Xamarin.Android.Arch.Lifecycle.Common.dll => 0x88ec8a16fd6b6644 => 38
	i64 9875200773399460291, ; 195: Xamarin.GooglePlayServices.Base.dll => 0x890bc2c8482339c3 => 146
	i64 9907349773706910547, ; 196: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x897dfa20b758db53 => 98
	i64 9998632235833408227, ; 197: Mono.Security => 0x8ac2470b209ebae3 => 180
	i64 10038780035334861115, ; 198: System.Net.Http.dll => 0x8b50e941206af13b => 30
	i64 10226222362177979215, ; 199: Xamarin.Kotlin.StdLib.Jdk7 => 0x8dead70ebbc6434f => 153
	i64 10229024438826829339, ; 200: Xamarin.AndroidX.CustomView => 0x8df4cb880b10061b => 93
	i64 10303855825347935641, ; 201: Xamarin.Android.Support.Loader => 0x8efea647eeb3fd99 => 59
	i64 10321854143672141184, ; 202: Xamarin.Jetbrains.Annotations.dll => 0x8f3e97a7f8f8c580 => 150
	i64 10360651442923773544, ; 203: System.Text.Encoding => 0x8fc86d98211c1e68 => 179
	i64 10363495123250631811, ; 204: Xamarin.Android.Support.Collections.dll => 0x8fd287e80cd8d483 => 46
	i64 10376576884623852283, ; 205: Xamarin.AndroidX.Tracing.Tracing => 0x900101b2f888c2fb => 129
	i64 10406448008575299332, ; 206: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x906b2153fcb3af04 => 156
	i64 10430153318873392755, ; 207: Xamarin.AndroidX.Core => 0x90bf592ea44f6673 => 91
	i64 10566960649245365243, ; 208: System.Globalization.dll => 0x92a562b96dcd13fb => 167
	i64 10635644668885628703, ; 209: Xamarin.Android.Support.DrawerLayout.dll => 0x93996679ee34771f => 56
	i64 10714184849103829812, ; 210: System.Runtime.Extensions.dll => 0x94b06e5aa4b4bb34 => 169
	i64 10775409704848971057, ; 211: Xamarin.Forms.Maps => 0x9589f20936d1d531 => 139
	i64 10847732767863316357, ; 212: Xamarin.AndroidX.Arch.Core.Common => 0x968ae37a86db9f85 => 80
	i64 10850923258212604222, ; 213: Xamarin.Android.Arch.Lifecycle.Runtime => 0x9696393672c9593e => 41
	i64 10913891249535884439, ; 214: Xamarin.Android.Support.CustomTabs.dll => 0x9775ee4465d49c97 => 52
	i64 11023048688141570732, ; 215: System.Core => 0x98f9bc61168392ac => 27
	i64 11037814507248023548, ; 216: System.Xml => 0x992e31d0412bf7fc => 34
	i64 11162124722117608902, ; 217: Xamarin.AndroidX.ViewPager => 0x9ae7d54b986d05c6 => 134
	i64 11340910727871153756, ; 218: Xamarin.AndroidX.CursorAdapter => 0x9d630238642d465c => 92
	i64 11347436699239206956, ; 219: System.Xml.XmlSerializer.dll => 0x9d7a318e8162502c => 166
	i64 11376461258732682436, ; 220: Xamarin.Android.Support.Compat => 0x9de14f3d5fc13cc4 => 47
	i64 11392833485892708388, ; 221: Xamarin.AndroidX.Print.dll => 0x9e1b79b18fcf6824 => 121
	i64 11395105072750394936, ; 222: Xamarin.Android.Support.v7.CardView => 0x9e238bb09789fe38 => 68
	i64 11444370155346000636, ; 223: Xamarin.Forms.Maps.Android => 0x9ed292057b7afafc => 138
	i64 11446671985764974897, ; 224: Mono.Android.Export => 0x9edabf8623efc131 => 13
	i64 11529969570048099689, ; 225: Xamarin.AndroidX.ViewPager.dll => 0xa002ae3c4dc7c569 => 134
	i64 11578238080964724296, ; 226: Xamarin.AndroidX.Legacy.Support.V4 => 0xa0ae2a30c4cd8648 => 103
	i64 11580057168383206117, ; 227: Xamarin.AndroidX.Annotation => 0xa0b4a0a4103262e5 => 76
	i64 11591352189662810718, ; 228: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0xa0dcc167234c525e => 127
	i64 11597940890313164233, ; 229: netstandard => 0xa0f429ca8d1805c9 => 172
	i64 11672361001936329215, ; 230: Xamarin.AndroidX.Interpolator => 0xa1fc8e7d0a8999ff => 100
	i64 11724931932191659106, ; 231: Xamarin.AndroidX.Palette => 0xa2b7537891eb1462 => 119
	i64 11743665907891708234, ; 232: System.Threading.Tasks => 0xa2f9e1ec30c0214a => 160
	i64 11834399401546345650, ; 233: Xamarin.Android.Support.SlidingPaneLayout.dll => 0xa43c3b8deb43ecb2 => 63
	i64 11865714326292153359, ; 234: Xamarin.Android.Arch.Lifecycle.LiveData => 0xa4ab7c5000e8440f => 40
	i64 11866610289935986454, ; 235: Xamarin.Android.Support.v7.Palette.dll => 0xa4aeab2fcba12f16 => 70
	i64 11876580865717101004, ; 236: ParseManager.dll => 0xa4d2175f5f44e1cc => 19
	i64 12123043025855404482, ; 237: System.Reflection.Extensions.dll => 0xa83db366c0e359c2 => 6
	i64 12137774235383566651, ; 238: Xamarin.AndroidX.VectorDrawable => 0xa872095bbfed113b => 132
	i64 12232655692966228690, ; 239: Syncfusion.SfGauge.XForms.Android.dll => 0xa9c31f8a96e972d2 => 25
	i64 12388767885335911387, ; 240: Xamarin.Android.Arch.Lifecycle.LiveData.dll => 0xabedbec0d236dbdb => 40
	i64 12414299427252656003, ; 241: Xamarin.Android.Support.Compat.dll => 0xac48738e28bad783 => 47
	i64 12451044538927396471, ; 242: Xamarin.AndroidX.Fragment.dll => 0xaccaff0a2955b677 => 99
	i64 12466513435562512481, ; 243: Xamarin.AndroidX.Loader.dll => 0xad01f3eb52569061 => 111
	i64 12487638416075308985, ; 244: Xamarin.AndroidX.DocumentFile.dll => 0xad4d00fa21b0bfb9 => 94
	i64 12538491095302438457, ; 245: Xamarin.AndroidX.CardView.dll => 0xae01ab382ae67e39 => 84
	i64 12550732019250633519, ; 246: System.IO.Compression => 0xae2d28465e8e1b2f => 29
	i64 12559163541709922900, ; 247: Xamarin.Android.Support.v7.CardView.dll => 0xae4b1cb32ba82254 => 68
	i64 12700543734426720211, ; 248: Xamarin.AndroidX.Collection => 0xb041653c70d157d3 => 85
	i64 12708238894395270091, ; 249: System.IO => 0xb05cbbf17d3ba3cb => 165
	i64 12828192437253469131, ; 250: Xamarin.Kotlin.StdLib.Jdk8.dll => 0xb206e50e14d873cb => 154
	i64 12952608645614506925, ; 251: Xamarin.Android.Support.Core.Utils => 0xb3c0e8eff48193ad => 50
	i64 12963446364377008305, ; 252: System.Drawing.Common.dll => 0xb3e769c8fd8548b1 => 159
	i64 13011563728930061243, ; 253: OxyPlot.dll => 0xb4925c45f33bbbbb => 17
	i64 13129914918964716986, ; 254: Xamarin.AndroidX.Emoji2.dll => 0xb636d40db3fe65ba => 97
	i64 13358059602087096138, ; 255: Xamarin.Android.Support.Fragment.dll => 0xb9615c6f1ee5af4a => 57
	i64 13370592475155966277, ; 256: System.Runtime.Serialization => 0xb98de304062ea945 => 9
	i64 13401370062847626945, ; 257: Xamarin.AndroidX.VectorDrawable.dll => 0xb9fb3b1193964ec1 => 132
	i64 13404347523447273790, ; 258: Xamarin.AndroidX.ConstraintLayout.Core => 0xba05cf0da4f6393e => 87
	i64 13454009404024712428, ; 259: Xamarin.Google.Guava.ListenableFuture => 0xbab63e4543a86cec => 145
	i64 13465488254036897740, ; 260: Xamarin.Kotlin.StdLib => 0xbadf06394d106fcc => 152
	i64 13491513212026656886, ; 261: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0xbb3b7bc905569876 => 81
	i64 13542075449887264013, ; 262: Parse.Android.dll => 0xbbef1ddf68118d0d => 7
	i64 13572454107664307259, ; 263: Xamarin.AndroidX.RecyclerView.dll => 0xbc5b0b19d99f543b => 123
	i64 13597739122007437430, ; 264: NavitasBeta.Android.dll => 0xbcb4dfb003af7076 => 0
	i64 13622732128915678507, ; 265: Syncfusion.Core.XForms.Android => 0xbd0daab1e651e52b => 21
	i64 13647894001087880694, ; 266: System.Data.dll => 0xbd670f48cb071df6 => 173
	i64 13702626353344114072, ; 267: System.Diagnostics.Tools.dll => 0xbe29821198fb6d98 => 2
	i64 13828521679616088467, ; 268: Xamarin.Kotlin.StdLib.Common => 0xbfe8c733724e1993 => 151
	i64 13959074834287824816, ; 269: Xamarin.AndroidX.Fragment => 0xc1b8989a7ad20fb0 => 99
	i64 13967638549803255703, ; 270: Xamarin.Forms.Platform.Android => 0xc1d70541e0134797 => 141
	i64 13970307180132182141, ; 271: Syncfusion.Licensing => 0xc1e0805ccade287d => 23
	i64 14124974489674258913, ; 272: Xamarin.AndroidX.CardView => 0xc405fd76067d19e1 => 84
	i64 14125464355221830302, ; 273: System.Threading.dll => 0xc407bafdbc707a9e => 164
	i64 14172845254133543601, ; 274: Xamarin.AndroidX.MultiDex => 0xc4b00faaed35f2b1 => 115
	i64 14261073672896646636, ; 275: Xamarin.AndroidX.Print => 0xc5e982f274ae0dec => 121
	i64 14327695147300244862, ; 276: System.Reflection.dll => 0xc6d632d338eb4d7e => 163
	i64 14369828458497533121, ; 277: Xamarin.Android.Support.Vector.Drawable => 0xc76be2d9300b64c1 => 72
	i64 14400856865250966808, ; 278: Xamarin.Android.Support.Core.UI => 0xc7da1f051a877d18 => 49
	i64 14486659737292545672, ; 279: Xamarin.AndroidX.Lifecycle.LiveData => 0xc90af44707469e88 => 106
	i64 14495724990987328804, ; 280: Xamarin.AndroidX.ResourceInspection.Annotation => 0xc92b2913e18d5d24 => 124
	i64 14538127318538747197, ; 281: Syncfusion.Licensing.dll => 0xc9c1cdc518e77d3d => 23
	i64 14613964489274772158, ; 282: Syncfusion.SfGauge.Android => 0xcacf3b465f654abe => 24
	i64 14644440854989303794, ; 283: Xamarin.AndroidX.LocalBroadcastManager.dll => 0xcb3b815e37daeff2 => 112
	i64 14661790646341542033, ; 284: Xamarin.Android.Support.SwipeRefreshLayout => 0xcb7924e94e552091 => 64
	i64 14763643331770587208, ; 285: OxyPlot.Xamarin.Forms.dll => 0xcce2ff639cc01848 => 4
	i64 14792063746108907174, ; 286: Xamarin.Google.Guava.ListenableFuture.dll => 0xcd47f79af9c15ea6 => 145
	i64 14852515768018889994, ; 287: Xamarin.AndroidX.CursorAdapter.dll => 0xce1ebc6625a76d0a => 92
	i64 14987728460634540364, ; 288: System.IO.Compression.dll => 0xcfff1ba06622494c => 29
	i64 14988210264188246988, ; 289: Xamarin.AndroidX.DocumentFile => 0xd000d1d307cddbcc => 94
	i64 15074835606759062662, ; 290: Parse.Android => 0xd134931d4c48bc86 => 7
	i64 15076659072870671916, ; 291: System.ObjectModel.dll => 0xd13b0d8c1620662c => 168
	i64 15133485256822086103, ; 292: System.Linq.dll => 0xd204f0a9127dd9d7 => 170
	i64 15150743910298169673, ; 293: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xd2424150783c3149 => 122
	i64 15188640517174936311, ; 294: Xamarin.Android.Arch.Core.Common => 0xd2c8e413d75142f7 => 36
	i64 15246441518555807158, ; 295: Xamarin.Android.Arch.Core.Common.dll => 0xd3963dc832493db6 => 36
	i64 15279429628684179188, ; 296: Xamarin.KotlinX.Coroutines.Android.dll => 0xd40b704b1c4c96f4 => 155
	i64 15326820765897713587, ; 297: Xamarin.Android.Arch.Core.Runtime.dll => 0xd4b3ce481769e7b3 => 37
	i64 15370334346939861994, ; 298: Xamarin.AndroidX.Core.dll => 0xd54e65a72c560bea => 91
	i64 15418891414777631748, ; 299: Xamarin.Android.Support.Transition => 0xd5fae80c88241404 => 65
	i64 15457813392950723921, ; 300: Xamarin.Android.Support.Media.Compat => 0xd6852f61c31a8551 => 61
	i64 15526743539506359484, ; 301: System.Text.Encoding.dll => 0xd77a12fc26de2cbc => 179
	i64 15568534730848034786, ; 302: Xamarin.Android.Support.VersionedParcelable.dll => 0xd80e8bda21875fe2 => 73
	i64 15582737692548360875, ; 303: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xd841015ed86f6aab => 110
	i64 15609085926864131306, ; 304: System.dll => 0xd89e9cf3334914ea => 28
	i64 15661133872274321916, ; 305: System.Xml.ReaderWriter.dll => 0xd9578647d4bfb1fc => 171
	i64 15777549416145007739, ; 306: Xamarin.AndroidX.SlidingPaneLayout.dll => 0xdaf51d99d77eb47b => 126
	i64 15810740023422282496, ; 307: Xamarin.Forms.Xaml => 0xdb6b08484c22eb00 => 143
	i64 15817206913877585035, ; 308: System.Threading.Tasks.dll => 0xdb8201e29086ac8b => 160
	i64 15930129725311349754, ; 309: Xamarin.GooglePlayServices.Tasks.dll => 0xdd1330956f12f3fa => 149
	i64 16154507427712707110, ; 310: System => 0xe03056ea4e39aa26 => 28
	i64 16242842420508142678, ; 311: Xamarin.Android.Support.CoordinaterLayout.dll => 0xe16a2b1f8908ac56 => 48
	i64 16308634204238690626, ; 312: ParseManager => 0xe253e866e36eb942 => 19
	i64 16423015068819898779, ; 313: Xamarin.Kotlin.StdLib.Jdk8 => 0xe3ea453135e5c19b => 154
	i64 16496768397145114574, ; 314: Mono.Android.Export.dll => 0xe4f04b741db987ce => 13
	i64 16565028646146589191, ; 315: System.ComponentModel.Composition.dll => 0xe5e2cdc9d3bcc207 => 177
	i64 16621146507174665210, ; 316: Xamarin.AndroidX.ConstraintLayout => 0xe6aa2caf87dedbfa => 88
	i64 16675739520757815591, ; 317: ParseManager.Droid.dll => 0xe76c20be05b65927 => 20
	i64 16677317093839702854, ; 318: Xamarin.AndroidX.Navigation.UI => 0xe771bb8960dd8b46 => 118
	i64 16702652415771857902, ; 319: System.ValueTuple => 0xe7cbbde0b0e6d3ee => 3
	i64 16767985610513713374, ; 320: Xamarin.Android.Arch.Core.Runtime => 0xe8b3da12798808de => 37
	i64 16822611501064131242, ; 321: System.Data.DataSetExtensions => 0xe975ec07bb5412aa => 175
	i64 16833383113903931215, ; 322: mscorlib => 0xe99c30c1484d7f4f => 14
	i64 16890310621557459193, ; 323: System.Text.RegularExpressions.dll => 0xea66700587f088f9 => 161
	i64 16932527889823454152, ; 324: Xamarin.Android.Support.Core.Utils.dll => 0xeafc6c67465253c8 => 50
	i64 17009591894298689098, ; 325: Xamarin.Android.Support.Animated.Vector.Drawable => 0xec0e35b50a097e4a => 43
	i64 17024911836938395553, ; 326: Xamarin.AndroidX.Annotation.Experimental.dll => 0xec44a31d250e5fa1 => 77
	i64 17031351772568316411, ; 327: Xamarin.AndroidX.Navigation.Common.dll => 0xec5b843380a769fb => 116
	i64 17037200463775726619, ; 328: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xec704b8e0a78fc1b => 102
	i64 17211698044874985152, ; 329: OxyPlot.Xamarin.Android => 0xeedc3c2e2a0f0ec0 => 8
	i64 17264113943957506899, ; 330: NavitasBeta.dll => 0xef967429af748b53 => 15
	i64 17383232329670771222, ; 331: Xamarin.Android.Support.CustomView.dll => 0xf13da5b41a1cc216 => 53
	i64 17428701562824544279, ; 332: Xamarin.Android.Support.Core.UI.dll => 0xf1df2fbaec73d017 => 49
	i64 17483646997724851973, ; 333: Xamarin.Android.Support.ViewPager => 0xf2a2644fe5b6ef05 => 74
	i64 17524135665394030571, ; 334: Xamarin.Android.Support.Print => 0xf3323c8a739097eb => 62
	i64 17544493274320527064, ; 335: Xamarin.AndroidX.AsyncLayoutInflater => 0xf37a8fada41aded8 => 82
	i64 17627500474728259406, ; 336: System.Globalization => 0xf4a176498a351f4e => 167
	i64 17666959971718154066, ; 337: Xamarin.Android.Support.CustomView => 0xf52da67d9f4e4752 => 53
	i64 17685921127322830888, ; 338: System.Diagnostics.Debug.dll => 0xf571038fafa74828 => 157
	i64 17704177640604968747, ; 339: Xamarin.AndroidX.Loader => 0xf5b1dfc36cac272b => 111
	i64 17710060891934109755, ; 340: Xamarin.AndroidX.Lifecycle.ViewModel => 0xf5c6c68c9e45303b => 109
	i64 17760961058993581169, ; 341: Xamarin.Android.Arch.Lifecycle.Common => 0xf67b9bfb46dbac71 => 38
	i64 17816041456001989629, ; 342: Xamarin.Forms.Maps.dll => 0xf73f4b4f90a1bbfd => 139
	i64 17841643939744178149, ; 343: Xamarin.Android.Arch.Lifecycle.ViewModel => 0xf79a40a25573dfe5 => 42
	i64 17882897186074144999, ; 344: FormsViewGroup => 0xf82cd03e3ac830e7 => 10
	i64 17891337867145587222, ; 345: Xamarin.Jetbrains.Annotations => 0xf84accff6fb52a16 => 150
	i64 17892495832318972303, ; 346: Xamarin.Forms.Xaml.dll => 0xf84eea293687918f => 143
	i64 17928294245072900555, ; 347: System.IO.Compression.FileSystem.dll => 0xf8ce18a0b24011cb => 176
	i64 17936749993673010118, ; 348: Xamarin.Android.Support.Design.dll => 0xf8ec231615deabc6 => 54
	i64 17956840076609788800, ; 349: OxyPlot.Xamarin.Forms.Platform.Android.dll => 0xf93382e906d31b80 => 18
	i64 17958105683855786126, ; 350: Xamarin.Android.Arch.Lifecycle.LiveData.Core.dll => 0xf93801f92d25c08e => 39
	i64 17969331831154222830, ; 351: Xamarin.GooglePlayServices.Maps => 0xf95fe418471126ee => 148
	i64 17986907704309214542, ; 352: Xamarin.GooglePlayServices.Basement.dll => 0xf99e554223166d4e => 147
	i64 18025913125965088385, ; 353: System.Threading => 0xfa28e87b91334681 => 164
	i64 18090425465832348288, ; 354: Xamarin.Android.Support.v7.RecyclerView => 0xfb0e1a1d2e9e1a80 => 71
	i64 18116111925905154859, ; 355: Xamarin.AndroidX.Arch.Core.Runtime => 0xfb695bd036cb632b => 81
	i64 18121036031235206392, ; 356: Xamarin.AndroidX.Navigation.Common => 0xfb7ada42d3d42cf8 => 116
	i64 18129453464017766560, ; 357: System.ServiceModel.Internals.dll => 0xfb98c1df1ec108a0 => 158
	i64 18260797123374478311, ; 358: Xamarin.AndroidX.Emoji2 => 0xfd6b623bde35f3e7 => 97
	i64 18301997741680159453, ; 359: Xamarin.Android.Support.CursorAdapter => 0xfdfdc1fa58d8eadd => 51
	i64 18305135509493619199, ; 360: Xamarin.AndroidX.Navigation.Runtime.dll => 0xfe08e7c2d8c199ff => 117
	i64 18380184030268848184 ; 361: Xamarin.AndroidX.VersionedParcelable => 0xff1387fe3e7b7838 => 133
], align 16
@assembly_image_cache_indices = local_unnamed_addr constant [362 x i32] [
	i32 96, i32 140, i32 12, i32 85, i32 3, i32 125, i32 126, i32 90, ; 0..7
	i32 114, i32 26, i32 42, i32 18, i32 108, i32 146, i32 159, i32 101, ; 8..15
	i32 5, i32 46, i32 39, i32 95, i32 174, i32 142, i32 180, i32 144, ; 16..23
	i32 80, i32 57, i32 9, i32 73, i32 78, i32 110, i32 102, i32 16, ; 24..31
	i32 67, i32 79, i32 125, i32 71, i32 76, i32 66, i32 109, i32 153, ; 32..39
	i32 22, i32 16, i32 115, i32 83, i32 95, i32 178, i32 124, i32 105, ; 40..47
	i32 21, i32 89, i32 131, i32 33, i32 75, i32 34, i32 14, i32 120, ; 48..55
	i32 127, i32 63, i32 137, i32 56, i32 144, i32 61, i32 104, i32 77, ; 56..63
	i32 32, i32 156, i32 147, i32 129, i32 128, i32 31, i32 33, i32 171, ; 64..71
	i32 172, i32 123, i32 96, i32 2, i32 5, i32 90, i32 25, i32 149, ; 72..79
	i32 74, i32 65, i32 55, i32 45, i32 158, i32 133, i32 166, i32 107, ; 80..87
	i32 82, i32 75, i32 128, i32 48, i32 60, i32 1, i32 1, i32 141, ; 88..95
	i32 62, i32 136, i32 112, i32 138, i32 17, i32 58, i32 113, i32 131, ; 96..103
	i32 130, i32 43, i32 157, i32 88, i32 27, i32 119, i32 135, i32 86, ; 104..111
	i32 44, i32 120, i32 106, i32 64, i32 142, i32 10, i32 60, i32 108, ; 112..119
	i32 105, i32 72, i32 70, i32 83, i32 93, i32 66, i32 178, i32 101, ; 120..127
	i32 24, i32 35, i32 32, i32 26, i32 22, i32 103, i32 15, i32 100, ; 128..135
	i32 148, i32 69, i32 51, i32 170, i32 163, i32 35, i32 168, i32 20, ; 136..143
	i32 137, i32 136, i32 30, i32 152, i32 173, i32 45, i32 78, i32 67, ; 144..151
	i32 69, i32 177, i32 162, i32 104, i32 44, i32 175, i32 6, i32 11, ; 152..159
	i32 162, i32 155, i32 58, i32 169, i32 4, i32 41, i32 87, i32 118, ; 160..167
	i32 107, i32 0, i32 98, i32 130, i32 11, i32 117, i32 176, i32 52, ; 168..175
	i32 59, i32 151, i32 55, i32 122, i32 114, i32 8, i32 140, i32 135, ; 176..183
	i32 79, i32 54, i32 165, i32 161, i32 31, i32 89, i32 113, i32 12, ; 184..191
	i32 86, i32 174, i32 38, i32 146, i32 98, i32 180, i32 30, i32 153, ; 192..199
	i32 93, i32 59, i32 150, i32 179, i32 46, i32 129, i32 156, i32 91, ; 200..207
	i32 167, i32 56, i32 169, i32 139, i32 80, i32 41, i32 52, i32 27, ; 208..215
	i32 34, i32 134, i32 92, i32 166, i32 47, i32 121, i32 68, i32 138, ; 216..223
	i32 13, i32 134, i32 103, i32 76, i32 127, i32 172, i32 100, i32 119, ; 224..231
	i32 160, i32 63, i32 40, i32 70, i32 19, i32 6, i32 132, i32 25, ; 232..239
	i32 40, i32 47, i32 99, i32 111, i32 94, i32 84, i32 29, i32 68, ; 240..247
	i32 85, i32 165, i32 154, i32 50, i32 159, i32 17, i32 97, i32 57, ; 248..255
	i32 9, i32 132, i32 87, i32 145, i32 152, i32 81, i32 7, i32 123, ; 256..263
	i32 0, i32 21, i32 173, i32 2, i32 151, i32 99, i32 141, i32 23, ; 264..271
	i32 84, i32 164, i32 115, i32 121, i32 163, i32 72, i32 49, i32 106, ; 272..279
	i32 124, i32 23, i32 24, i32 112, i32 64, i32 4, i32 145, i32 92, ; 280..287
	i32 29, i32 94, i32 7, i32 168, i32 170, i32 122, i32 36, i32 36, ; 288..295
	i32 155, i32 37, i32 91, i32 65, i32 61, i32 179, i32 73, i32 110, ; 296..303
	i32 28, i32 171, i32 126, i32 143, i32 160, i32 149, i32 28, i32 48, ; 304..311
	i32 19, i32 154, i32 13, i32 177, i32 88, i32 20, i32 118, i32 3, ; 312..319
	i32 37, i32 175, i32 14, i32 161, i32 50, i32 43, i32 77, i32 116, ; 320..327
	i32 102, i32 8, i32 15, i32 53, i32 49, i32 74, i32 62, i32 82, ; 328..335
	i32 167, i32 53, i32 157, i32 111, i32 109, i32 38, i32 139, i32 42, ; 336..343
	i32 10, i32 150, i32 143, i32 176, i32 54, i32 18, i32 39, i32 148, ; 344..351
	i32 147, i32 164, i32 71, i32 81, i32 116, i32 158, i32 97, i32 51, ; 352..359
	i32 117, i32 133 ; 360..361
], align 16

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 8; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 8

; Function attributes: "frame-pointer"="none" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 8
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 8
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 16; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="none" "target-cpu"="x86-64" "target-features"="+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="none" "target-cpu"="x86-64" "target-features"="+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
