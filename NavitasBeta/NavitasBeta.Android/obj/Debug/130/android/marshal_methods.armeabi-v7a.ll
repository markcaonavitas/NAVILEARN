; ModuleID = 'obj\Debug\130\android\marshal_methods.armeabi-v7a.ll'
source_filename = "obj\Debug\130\android\marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android"


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
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 4
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [362 x i32] [
	i32 11257817, ; 0: OxyPlot.dll => 0xabc7d9 => 17
	i32 32687329, ; 1: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 108
	i32 34715100, ; 2: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 145
	i32 39109920, ; 3: Newtonsoft.Json.dll => 0x254c520 => 16
	i32 57263871, ; 4: Xamarin.Forms.Core.dll => 0x369c6ff => 137
	i32 57967248, ; 5: Xamarin.Android.Support.VersionedParcelable.dll => 0x3748290 => 73
	i32 101534019, ; 6: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 126
	i32 120558881, ; 7: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 126
	i32 134690465, ; 8: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 153
	i32 160529393, ; 9: Xamarin.Android.Arch.Core.Common => 0x9917bf1 => 36
	i32 165246403, ; 10: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 85
	i32 166922606, ; 11: Xamarin.Android.Support.Compat.dll => 0x9f3096e => 47
	i32 182336117, ; 12: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 128
	i32 184328833, ; 13: System.ValueTuple.dll => 0xafca281 => 3
	i32 201930040, ; 14: Xamarin.Android.Arch.Core.Runtime => 0xc093538 => 37
	i32 205061960, ; 15: System.ComponentModel => 0xc38ff48 => 5
	i32 209399409, ; 16: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 83
	i32 212497893, ; 17: Xamarin.Forms.Maps.Android => 0xcaa75e5 => 138
	i32 219130465, ; 18: Xamarin.Android.Support.v4 => 0xd0faa61 => 66
	i32 220171995, ; 19: System.Diagnostics.Debug => 0xd1f8edb => 157
	i32 230216969, ; 20: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 102
	i32 231814094, ; 21: System.Globalization => 0xdd133ce => 167
	i32 232587938, ; 22: Xamarin.AndroidX.MediaRouter => 0xddd02a2 => 114
	i32 232815796, ; 23: System.Web.Services => 0xde07cb4 => 178
	i32 261689757, ; 24: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 88
	i32 278686392, ; 25: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 106
	i32 280482487, ; 26: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 100
	i32 293914992, ; 27: Xamarin.Android.Support.Transition => 0x1184c970 => 65
	i32 318968648, ; 28: Xamarin.AndroidX.Activity.dll => 0x13031348 => 75
	i32 319314094, ; 29: Xamarin.Forms.Maps => 0x130858ae => 139
	i32 321597661, ; 30: System.Numerics => 0x132b30dd => 31
	i32 331266987, ; 31: Xamarin.Android.Support.v7.MediaRouter.dll => 0x13bebbab => 69
	i32 342366114, ; 32: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 104
	i32 381494705, ; 33: Xamarin.Forms.Material => 0x16bd25b1 => 140
	i32 388313361, ; 34: Xamarin.Android.Support.Animated.Vector.Drawable => 0x17253111 => 43
	i32 389971796, ; 35: Xamarin.Android.Support.Core.UI => 0x173e7f54 => 49
	i32 441335492, ; 36: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 87
	i32 442521989, ; 37: Xamarin.Essentials => 0x1a605985 => 136
	i32 442565967, ; 38: System.Collections => 0x1a61054f => 162
	i32 450948140, ; 39: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 99
	i32 465846621, ; 40: mscorlib => 0x1bc4415d => 14
	i32 469710990, ; 41: System.dll => 0x1bff388e => 28
	i32 476646585, ; 42: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 100
	i32 486930444, ; 43: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 112
	i32 498788369, ; 44: System.ObjectModel => 0x1dbae811 => 168
	i32 514659665, ; 45: Xamarin.Android.Support.Compat => 0x1ead1551 => 47
	i32 524864063, ; 46: Xamarin.Android.Support.Print => 0x1f48ca3f => 62
	i32 526420162, ; 47: System.Transactions.dll => 0x1f6088c2 => 174
	i32 527452488, ; 48: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 153
	i32 529739732, ; 49: ParseManager.dll => 0x1f932fd4 => 19
	i32 539750087, ; 50: Xamarin.Android.Support.Design => 0x202beec7 => 54
	i32 545304856, ; 51: System.Runtime.Extensions => 0x2080b118 => 169
	i32 571524804, ; 52: Xamarin.Android.Support.v7.RecyclerView => 0x2210c6c4 => 71
	i32 586291505, ; 53: NavitasBeta => 0x22f21931 => 15
	i32 605376203, ; 54: System.IO.Compression.FileSystem => 0x24154ecb => 176
	i32 627609679, ; 55: Xamarin.AndroidX.CustomView => 0x2568904f => 93
	i32 639843206, ; 56: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 98
	i32 663517072, ; 57: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 133
	i32 666292255, ; 58: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 80
	i32 690569205, ; 59: System.Xml.Linq.dll => 0x29293ff5 => 35
	i32 691348768, ; 60: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 155
	i32 692692150, ; 61: Xamarin.Android.Support.Annotations => 0x2949a4b6 => 44
	i32 700284507, ; 62: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 150
	i32 719061231, ; 63: Syncfusion.Core.XForms.dll => 0x2adc00ef => 22
	i32 720511267, ; 64: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 154
	i32 775507847, ; 65: System.IO.Compression => 0x2e394f87 => 29
	i32 801787702, ; 66: Xamarin.Android.Support.Interpolator => 0x2fca4f36 => 58
	i32 809851609, ; 67: System.Drawing.Common.dll => 0x30455ad9 => 159
	i32 843511501, ; 68: Xamarin.AndroidX.Print => 0x3246f6cd => 121
	i32 877678880, ; 69: System.Globalization.dll => 0x34505120 => 167
	i32 882883187, ; 70: Xamarin.Android.Support.v4.dll => 0x349fba73 => 66
	i32 890949947, ; 71: ParseManager.Droid => 0x351ad13b => 20
	i32 916714535, ; 72: Xamarin.Android.Support.Print.dll => 0x36a3f427 => 62
	i32 928116545, ; 73: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 145
	i32 955402788, ; 74: Newtonsoft.Json => 0x38f24a24 => 16
	i32 956575887, ; 75: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 154
	i32 958213972, ; 76: Xamarin.Android.Support.Media.Compat => 0x391d2f54 => 61
	i32 967690846, ; 77: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 104
	i32 974778368, ; 78: FormsViewGroup.dll => 0x3a19f000 => 10
	i32 987214855, ; 79: System.Diagnostics.Tools => 0x3ad7b407 => 2
	i32 987342438, ; 80: Xamarin.Android.Arch.Lifecycle.LiveData.dll => 0x3ad9a666 => 40
	i32 992768348, ; 81: System.Collections.dll => 0x3b2c715c => 162
	i32 1012816738, ; 82: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 125
	i32 1035644815, ; 83: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 79
	i32 1042160112, ; 84: Xamarin.Forms.Platform.dll => 0x3e1e19f0 => 142
	i32 1052210849, ; 85: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 109
	i32 1084122840, ; 86: Xamarin.Kotlin.StdLib => 0x409e66d8 => 152
	i32 1098167829, ; 87: Xamarin.Android.Arch.Lifecycle.ViewModel => 0x4174b615 => 42
	i32 1098259244, ; 88: System => 0x41761b2c => 28
	i32 1175144683, ; 89: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 131
	i32 1178241025, ; 90: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 117
	i32 1204270330, ; 91: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 80
	i32 1264511973, ; 92: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 127
	i32 1267360935, ; 93: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 132
	i32 1275534314, ; 94: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 155
	i32 1292763917, ; 95: Xamarin.Android.Support.CursorAdapter.dll => 0x4d0e030d => 51
	i32 1293217323, ; 96: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 95
	i32 1297413738, ; 97: Xamarin.Android.Arch.Lifecycle.LiveData.Core => 0x4d54f66a => 39
	i32 1324164729, ; 98: System.Linq => 0x4eed2679 => 170
	i32 1331773366, ; 99: Syncfusion.SfGauge.XForms => 0x4f613fb6 => 26
	i32 1359785034, ; 100: Xamarin.Android.Support.Design.dll => 0x510cac4a => 54
	i32 1364015309, ; 101: System.IO => 0x514d38cd => 165
	i32 1365406463, ; 102: System.ServiceModel.Internals.dll => 0x516272ff => 158
	i32 1376866003, ; 103: Xamarin.AndroidX.SavedState => 0x52114ed3 => 125
	i32 1379779777, ; 104: System.Resources.ResourceManager => 0x523dc4c1 => 1
	i32 1395857551, ; 105: Xamarin.AndroidX.Media.dll => 0x5333188f => 113
	i32 1406073936, ; 106: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 89
	i32 1445445088, ; 107: Xamarin.Android.Support.Fragment => 0x5627bde0 => 57
	i32 1453312822, ; 108: System.Diagnostics.Tools.dll => 0x569fcb36 => 2
	i32 1457743152, ; 109: System.Runtime.Extensions.dll => 0x56e36530 => 169
	i32 1460219004, ; 110: Xamarin.Forms.Xaml => 0x57092c7c => 143
	i32 1462112819, ; 111: System.IO.Compression.dll => 0x57261233 => 29
	i32 1469204771, ; 112: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 78
	i32 1516315406, ; 113: Syncfusion.Core.XForms.Android.dll => 0x5a61230e => 21
	i32 1530663695, ; 114: Xamarin.Forms.Maps.dll => 0x5b3c130f => 139
	i32 1543031311, ; 115: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 161
	i32 1550322496, ; 116: System.Reflection.Extensions.dll => 0x5c680b40 => 6
	i32 1574652163, ; 117: Xamarin.Android.Support.Core.Utils.dll => 0x5ddb4903 => 50
	i32 1582372066, ; 118: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 94
	i32 1587447679, ; 119: Xamarin.Android.Arch.Core.Common.dll => 0x5e9e877f => 36
	i32 1592978981, ; 120: System.Runtime.Serialization.dll => 0x5ef2ee25 => 9
	i32 1622152042, ; 121: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 111
	i32 1624863272, ; 122: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 135
	i32 1635184631, ; 123: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 98
	i32 1636350590, ; 124: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 92
	i32 1639515021, ; 125: System.Net.Http.dll => 0x61b9038d => 30
	i32 1639986890, ; 126: System.Text.RegularExpressions => 0x61c036ca => 161
	i32 1657153582, ; 127: System.Runtime => 0x62c6282e => 33
	i32 1658241508, ; 128: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 129
	i32 1658251792, ; 129: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 144
	i32 1662014763, ; 130: Xamarin.Android.Support.Vector.Drawable => 0x6310552b => 72
	i32 1670060433, ; 131: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 88
	i32 1698840827, ; 132: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 151
	i32 1701541528, ; 133: System.Diagnostics.Debug.dll => 0x656b7698 => 157
	i32 1726116996, ; 134: System.Reflection.dll => 0x66e27484 => 163
	i32 1729485958, ; 135: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 84
	i32 1746316138, ; 136: Mono.Android.Export => 0x6816ab6a => 13
	i32 1765942094, ; 137: System.Reflection.Extensions => 0x6942234e => 6
	i32 1766324549, ; 138: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 128
	i32 1776026572, ; 139: System.Core.dll => 0x69dc03cc => 27
	i32 1788241197, ; 140: Xamarin.AndroidX.Fragment => 0x6a96652d => 99
	i32 1798312337, ; 141: Syncfusion.SfGauge.XForms.dll => 0x6b301191 => 26
	i32 1808609942, ; 142: Xamarin.AndroidX.Loader => 0x6bcd3296 => 111
	i32 1813058853, ; 143: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 152
	i32 1813201214, ; 144: Xamarin.Google.Android.Material => 0x6c13413e => 144
	i32 1818569960, ; 145: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 118
	i32 1866717392, ; 146: Xamarin.Android.Support.Interpolator.dll => 0x6f43d8d0 => 58
	i32 1867746548, ; 147: Xamarin.Essentials.dll => 0x6f538cf4 => 136
	i32 1877418711, ; 148: Xamarin.Android.Support.v7.RecyclerView.dll => 0x6fe722d7 => 71
	i32 1878053835, ; 149: Xamarin.Forms.Xaml.dll => 0x6ff0d3cb => 143
	i32 1881862856, ; 150: Xamarin.Forms.Maps.Android.dll => 0x702af2c8 => 138
	i32 1885316902, ; 151: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 81
	i32 1900610850, ; 152: System.Resources.ResourceManager.dll => 0x71490522 => 1
	i32 1908813208, ; 153: Xamarin.GooglePlayServices.Basement => 0x71c62d98 => 147
	i32 1916660109, ; 154: Xamarin.Android.Arch.Lifecycle.ViewModel.dll => 0x723de98d => 42
	i32 1919157823, ; 155: Xamarin.AndroidX.MultiDex.dll => 0x7264063f => 115
	i32 1983156543, ; 156: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 151
	i32 2019465201, ; 157: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 109
	i32 2037417872, ; 158: Xamarin.Android.Support.ViewPager => 0x79708790 => 74
	i32 2044222327, ; 159: Xamarin.Android.Support.Loader => 0x79d85b77 => 59
	i32 2055257422, ; 160: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 105
	i32 2079903147, ; 161: System.Runtime.dll => 0x7bf8cdab => 33
	i32 2090596640, ; 162: System.Numerics.Vectors => 0x7c9bf920 => 32
	i32 2097448633, ; 163: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x7d0486b9 => 101
	i32 2126786730, ; 164: Xamarin.Forms.Platform.Android => 0x7ec430aa => 141
	i32 2129483829, ; 165: Xamarin.GooglePlayServices.Base.dll => 0x7eed5835 => 146
	i32 2139458754, ; 166: Xamarin.Android.Support.DrawerLayout => 0x7f858cc2 => 56
	i32 2143790110, ; 167: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 166
	i32 2166116741, ; 168: Xamarin.Android.Support.Core.Utils => 0x811c5185 => 50
	i32 2193016926, ; 169: System.ObjectModel.dll => 0x82b6c85e => 168
	i32 2196165013, ; 170: Xamarin.Android.Support.VersionedParcelable => 0x82e6d195 => 73
	i32 2201107256, ; 171: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 156
	i32 2201231467, ; 172: System.Net.Http => 0x8334206b => 30
	i32 2217644978, ; 173: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 131
	i32 2244775296, ; 174: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 112
	i32 2256548716, ; 175: Xamarin.AndroidX.MultiDex => 0x8680336c => 115
	i32 2261435625, ; 176: Xamarin.AndroidX.Legacy.Support.V4.dll => 0x86cac4e9 => 103
	i32 2279755925, ; 177: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 123
	i32 2306217607, ; 178: OxyPlot.Xamarin.Forms => 0x89761687 => 4
	i32 2315684594, ; 179: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 76
	i32 2330457430, ; 180: Xamarin.Android.Support.Core.UI.dll => 0x8ae7f556 => 49
	i32 2330986192, ; 181: Xamarin.Android.Support.SlidingPaneLayout.dll => 0x8af006d0 => 63
	i32 2343171156, ; 182: Syncfusion.Core.XForms => 0x8ba9f454 => 22
	i32 2344264397, ; 183: System.ValueTuple => 0x8bbaa2cd => 3
	i32 2354730003, ; 184: Syncfusion.Licensing => 0x8c5a5413 => 23
	i32 2368005991, ; 185: System.Xml.ReaderWriter.dll => 0x8d24e767 => 171
	i32 2373288475, ; 186: Xamarin.Android.Support.Fragment.dll => 0x8d75821b => 57
	i32 2403452196, ; 187: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 97
	i32 2409053734, ; 188: Xamarin.AndroidX.Preference.dll => 0x8f973e26 => 120
	i32 2425099629, ; 189: ParseManager.Droid.dll => 0x908c156d => 20
	i32 2440966767, ; 190: Xamarin.Android.Support.Loader.dll => 0x917e326f => 59
	i32 2454642406, ; 191: System.Text.Encoding.dll => 0x924edee6 => 179
	i32 2465532216, ; 192: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 87
	i32 2471841756, ; 193: netstandard.dll => 0x93554fdc => 172
	i32 2475788418, ; 194: Java.Interop.dll => 0x93918882 => 11
	i32 2487632829, ; 195: Xamarin.Android.Support.DocumentFile => 0x944643bd => 55
	i32 2501346920, ; 196: System.Data.DataSetExtensions => 0x95178668 => 175
	i32 2505896520, ; 197: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 108
	i32 2568748418, ; 198: OxyPlot.Xamarin.Forms.Platform.Android => 0x991bfd82 => 18
	i32 2581819634, ; 199: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 132
	i32 2605712449, ; 200: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 156
	i32 2620871830, ; 201: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 92
	i32 2624644809, ; 202: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 96
	i32 2626405577, ; 203: ParseManager => 0x9c8bc4c9 => 19
	i32 2633051222, ; 204: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 106
	i32 2689529426, ; 205: OxyPlot.Xamarin.Android => 0xa04ef652 => 8
	i32 2693849962, ; 206: System.IO.dll => 0xa090e36a => 165
	i32 2698266930, ; 207: Xamarin.Android.Arch.Lifecycle.LiveData => 0xa0d44932 => 40
	i32 2701096212, ; 208: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 129
	i32 2715334215, ; 209: System.Threading.Tasks.dll => 0xa1d8b647 => 160
	i32 2732626843, ; 210: Xamarin.AndroidX.Activity => 0xa2e0939b => 75
	i32 2737747696, ; 211: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 78
	i32 2747619038, ; 212: OxyPlot.Xamarin.Android.dll => 0xa3c556de => 8
	i32 2751899777, ; 213: Xamarin.Android.Support.Collections => 0xa406a881 => 46
	i32 2754271178, ; 214: Xamarin.Android.Support.v7.Palette => 0xa42ad7ca => 70
	i32 2766581644, ; 215: Xamarin.Forms.Core => 0xa4e6af8c => 137
	i32 2770495804, ; 216: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 150
	i32 2772484381, ; 217: Xamarin.AndroidX.Palette.dll => 0xa540c11d => 119
	i32 2778768386, ; 218: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 134
	i32 2779977773, ; 219: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 124
	i32 2782647110, ; 220: Xamarin.Android.Support.CustomTabs.dll => 0xa5dbd346 => 52
	i32 2788639665, ; 221: Xamarin.Android.Support.LocalBroadcastManager => 0xa63743b1 => 60
	i32 2788775637, ; 222: Xamarin.Android.Support.SwipeRefreshLayout.dll => 0xa63956d5 => 64
	i32 2810250172, ; 223: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 89
	i32 2819470561, ; 224: System.Xml.dll => 0xa80db4e1 => 34
	i32 2821294376, ; 225: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 124
	i32 2847418871, ; 226: Xamarin.GooglePlayServices.Base => 0xa9b829f7 => 146
	i32 2853208004, ; 227: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 134
	i32 2855708567, ; 228: Xamarin.AndroidX.Transition => 0xaa36a797 => 130
	i32 2861098320, ; 229: Mono.Android.Export.dll => 0xaa88e550 => 13
	i32 2868557005, ; 230: Syncfusion.Licensing.dll => 0xaafab4cd => 23
	i32 2874148507, ; 231: Syncfusion.Core.XForms.Android => 0xab50069b => 21
	i32 2876493027, ; 232: Xamarin.Android.Support.SwipeRefreshLayout => 0xab73cce3 => 64
	i32 2893257763, ; 233: Xamarin.Android.Arch.Core.Runtime.dll => 0xac739c23 => 37
	i32 2901442782, ; 234: System.Reflection => 0xacf080de => 163
	i32 2903344695, ; 235: System.ComponentModel.Composition => 0xad0d8637 => 177
	i32 2905242038, ; 236: mscorlib.dll => 0xad2a79b6 => 14
	i32 2916838712, ; 237: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 135
	i32 2919462931, ; 238: System.Numerics.Vectors.dll => 0xae037813 => 32
	i32 2921128767, ; 239: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 77
	i32 2921692953, ; 240: Xamarin.Android.Support.CustomView.dll => 0xae257f19 => 53
	i32 2922925221, ; 241: Xamarin.Android.Support.Vector.Drawable.dll => 0xae384ca5 => 72
	i32 2959614098, ; 242: System.ComponentModel.dll => 0xb0682092 => 5
	i32 2978675010, ; 243: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 95
	i32 2996846495, ; 244: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 107
	i32 3016983068, ; 245: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 127
	i32 3017076677, ; 246: Xamarin.GooglePlayServices.Maps => 0xb3d4efc5 => 148
	i32 3021342700, ; 247: Syncfusion.SfGauge.Android => 0xb41607ec => 24
	i32 3024354802, ; 248: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 102
	i32 3044182254, ; 249: FormsViewGroup => 0xb57288ee => 10
	i32 3056250942, ; 250: Xamarin.Android.Support.AsyncLayoutInflater.dll => 0xb62ab03e => 45
	i32 3057625584, ; 251: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 116
	i32 3058099980, ; 252: Xamarin.GooglePlayServices.Tasks => 0xb646e70c => 149
	i32 3068715062, ; 253: Xamarin.Android.Arch.Lifecycle.Common => 0xb6e8e036 => 38
	i32 3075834255, ; 254: System.Threading.Tasks => 0xb755818f => 160
	i32 3085392760, ; 255: OxyPlot => 0xb7e75b78 => 17
	i32 3092211740, ; 256: Xamarin.Android.Support.Media.Compat.dll => 0xb84f681c => 61
	i32 3111772706, ; 257: System.Runtime.Serialization => 0xb979e222 => 9
	i32 3147431871, ; 258: OxyPlot.Xamarin.Forms.dll => 0xbb99ffbf => 4
	i32 3191408315, ; 259: Xamarin.Android.Support.CustomTabs => 0xbe3906bb => 52
	i32 3194035187, ; 260: Xamarin.Android.Support.v7.MediaRouter => 0xbe611bf3 => 69
	i32 3204380047, ; 261: System.Data.dll => 0xbefef58f => 173
	i32 3204912593, ; 262: Xamarin.Android.Support.AsyncLayoutInflater => 0xbf0715d1 => 45
	i32 3211777861, ; 263: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 94
	i32 3220365878, ; 264: System.Threading => 0xbff2e236 => 164
	i32 3230466174, ; 265: Xamarin.GooglePlayServices.Basement.dll => 0xc08d007e => 147
	i32 3233339011, ; 266: Xamarin.Android.Arch.Lifecycle.LiveData.Core.dll => 0xc0b8d683 => 39
	i32 3247949154, ; 267: Mono.Security => 0xc197c562 => 180
	i32 3258312781, ; 268: Xamarin.AndroidX.CardView => 0xc235e84d => 84
	i32 3267021929, ; 269: Xamarin.AndroidX.AsyncLayoutInflater => 0xc2bacc69 => 82
	i32 3296380511, ; 270: Xamarin.Android.Support.Collections.dll => 0xc47ac65f => 46
	i32 3299363146, ; 271: System.Text.Encoding => 0xc4a8494a => 179
	i32 3317135071, ; 272: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 93
	i32 3317144872, ; 273: System.Data => 0xc5b79d28 => 173
	i32 3321585405, ; 274: Xamarin.Android.Support.DocumentFile.dll => 0xc5fb5efd => 55
	i32 3338512932, ; 275: Syncfusion.SfGauge.XForms.Android.dll => 0xc6fdaa24 => 25
	i32 3340431453, ; 276: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 81
	i32 3345895724, ; 277: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 122
	i32 3346324047, ; 278: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 117
	i32 3352662376, ; 279: Xamarin.Android.Support.CoordinaterLayout => 0xc7d59168 => 48
	i32 3353484488, ; 280: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0xc7e21cc8 => 101
	i32 3357663996, ; 281: Xamarin.Android.Support.CursorAdapter => 0xc821e2fc => 51
	i32 3362522851, ; 282: Xamarin.AndroidX.Core => 0xc86c06e3 => 91
	i32 3366347497, ; 283: Java.Interop => 0xc8a662e9 => 11
	i32 3369739654, ; 284: Xamarin.AndroidX.Palette => 0xc8da2586 => 119
	i32 3374999561, ; 285: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 123
	i32 3404865022, ; 286: System.ServiceModel.Internals => 0xcaf21dfe => 158
	i32 3429136800, ; 287: System.Xml => 0xcc6479a0 => 34
	i32 3430777524, ; 288: netstandard => 0xcc7d82b4 => 172
	i32 3439690031, ; 289: Xamarin.Android.Support.Annotations.dll => 0xcd05812f => 44
	i32 3441283291, ; 290: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 96
	i32 3476120550, ; 291: Mono.Android => 0xcf3163e6 => 12
	i32 3486566296, ; 292: System.Transactions => 0xcfd0c798 => 174
	i32 3493954962, ; 293: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 86
	i32 3498942916, ; 294: Xamarin.Android.Support.v7.CardView.dll => 0xd08da1c4 => 68
	i32 3501239056, ; 295: Xamarin.AndroidX.AsyncLayoutInflater.dll => 0xd0b0ab10 => 82
	i32 3509114376, ; 296: System.Xml.Linq => 0xd128d608 => 35
	i32 3536029504, ; 297: Xamarin.Forms.Platform.Android.dll => 0xd2c38740 => 141
	i32 3547625832, ; 298: Xamarin.Android.Support.SlidingPaneLayout => 0xd3747968 => 63
	i32 3567349600, ; 299: System.ComponentModel.Composition.dll => 0xd4a16f60 => 177
	i32 3608519521, ; 300: System.Linq.dll => 0xd715a361 => 170
	i32 3618140916, ; 301: Xamarin.AndroidX.Preference => 0xd7a872f4 => 120
	i32 3627220390, ; 302: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 121
	i32 3629053394, ; 303: Xamarin.AndroidX.MediaRouter.dll => 0xd84ef5d2 => 114
	i32 3632359727, ; 304: Xamarin.Forms.Platform => 0xd881692f => 142
	i32 3633644679, ; 305: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 77
	i32 3636014592, ; 306: Syncfusion.SfGauge.Android.dll => 0xd8b92e00 => 24
	i32 3641597786, ; 307: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 105
	i32 3664423555, ; 308: Xamarin.Android.Support.ViewPager.dll => 0xda6aaa83 => 74
	i32 3672681054, ; 309: Mono.Android.dll => 0xdae8aa5e => 12
	i32 3676310014, ; 310: System.Web.Services.dll => 0xdb2009fe => 178
	i32 3678221644, ; 311: Xamarin.Android.Support.v7.AppCompat => 0xdb3d354c => 67
	i32 3681174138, ; 312: Xamarin.Android.Arch.Lifecycle.Common.dll => 0xdb6a427a => 38
	i32 3682565725, ; 313: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 83
	i32 3684561358, ; 314: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 86
	i32 3685813676, ; 315: Xamarin.Forms.Material.dll => 0xdbb10dac => 140
	i32 3689375977, ; 316: System.Drawing.Common => 0xdbe768e9 => 159
	i32 3706696989, ; 317: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 90
	i32 3714038699, ; 318: Xamarin.Android.Support.LocalBroadcastManager.dll => 0xdd5fbbab => 60
	i32 3718463572, ; 319: Xamarin.Android.Support.Animated.Vector.Drawable.dll => 0xdda34054 => 43
	i32 3718780102, ; 320: Xamarin.AndroidX.Annotation => 0xdda814c6 => 76
	i32 3722585637, ; 321: NavitasBeta.dll => 0xdde22625 => 15
	i32 3724971120, ; 322: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 116
	i32 3758932259, ; 323: Xamarin.AndroidX.Legacy.Support.V4 => 0xe00cc123 => 103
	i32 3776062606, ; 324: Xamarin.Android.Support.DrawerLayout.dll => 0xe112248e => 56
	i32 3786282454, ; 325: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 85
	i32 3789524022, ; 326: Xamarin.Android.Support.v7.Palette.dll => 0xe1df8c36 => 70
	i32 3822602673, ; 327: Xamarin.AndroidX.Media => 0xe3d849b1 => 113
	i32 3829621856, ; 328: System.Numerics.dll => 0xe4436460 => 31
	i32 3832731800, ; 329: Xamarin.Android.Support.CoordinaterLayout.dll => 0xe472d898 => 48
	i32 3836070875, ; 330: Syncfusion.SfGauge.XForms.Android => 0xe4a5cbdb => 25
	i32 3854864648, ; 331: OxyPlot.Xamarin.Forms.Platform.Android.dll => 0xe5c49108 => 18
	i32 3862817207, ; 332: Xamarin.Android.Arch.Lifecycle.Runtime.dll => 0xe63de9b7 => 41
	i32 3874897629, ; 333: Xamarin.Android.Arch.Lifecycle.Runtime => 0xe6f63edd => 41
	i32 3883175360, ; 334: Xamarin.Android.Support.v7.AppCompat.dll => 0xe7748dc0 => 67
	i32 3884566603, ; 335: NavitasBeta.Android => 0xe789c84b => 0
	i32 3885922214, ; 336: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 130
	i32 3888767677, ; 337: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 122
	i32 3896760992, ; 338: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 91
	i32 3920810846, ; 339: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 176
	i32 3921031405, ; 340: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 133
	i32 3928044579, ; 341: System.Xml.ReaderWriter => 0xea213423 => 171
	i32 3930117279, ; 342: Parse.Android => 0xea40d49f => 7
	i32 3931092270, ; 343: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 118
	i32 3936245417, ; 344: NavitasBeta.Android.dll => 0xea9e56a9 => 0
	i32 3945713374, ; 345: System.Data.DataSetExtensions.dll => 0xeb2ecede => 175
	i32 3955647286, ; 346: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 79
	i32 3959773229, ; 347: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 107
	i32 3970018735, ; 348: Xamarin.GooglePlayServices.Tasks.dll => 0xeca1adaf => 149
	i32 4063435586, ; 349: Xamarin.Android.Support.CustomView => 0xf2331b42 => 53
	i32 4073602200, ; 350: System.Threading.dll => 0xf2ce3c98 => 164
	i32 4101593132, ; 351: Xamarin.AndroidX.Emoji2 => 0xf479582c => 97
	i32 4105002889, ; 352: Mono.Security.dll => 0xf4ad5f89 => 180
	i32 4151237749, ; 353: System.Core => 0xf76edc75 => 27
	i32 4159265925, ; 354: System.Xml.XmlSerializer => 0xf7e95c85 => 166
	i32 4182413190, ; 355: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 110
	i32 4206536145, ; 356: Parse.Android.dll => 0xfabaa5d1 => 7
	i32 4216993138, ; 357: Xamarin.Android.Support.Transition.dll => 0xfb5a3572 => 65
	i32 4219003402, ; 358: Xamarin.Android.Support.v7.CardView => 0xfb78e20a => 68
	i32 4256097574, ; 359: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 90
	i32 4278134329, ; 360: Xamarin.GooglePlayServices.Maps.dll => 0xfeff2639 => 148
	i32 4292120959 ; 361: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 110
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [362 x i32] [
	i32 17, i32 108, i32 145, i32 16, i32 137, i32 73, i32 126, i32 126, ; 0..7
	i32 153, i32 36, i32 85, i32 47, i32 128, i32 3, i32 37, i32 5, ; 8..15
	i32 83, i32 138, i32 66, i32 157, i32 102, i32 167, i32 114, i32 178, ; 16..23
	i32 88, i32 106, i32 100, i32 65, i32 75, i32 139, i32 31, i32 69, ; 24..31
	i32 104, i32 140, i32 43, i32 49, i32 87, i32 136, i32 162, i32 99, ; 32..39
	i32 14, i32 28, i32 100, i32 112, i32 168, i32 47, i32 62, i32 174, ; 40..47
	i32 153, i32 19, i32 54, i32 169, i32 71, i32 15, i32 176, i32 93, ; 48..55
	i32 98, i32 133, i32 80, i32 35, i32 155, i32 44, i32 150, i32 22, ; 56..63
	i32 154, i32 29, i32 58, i32 159, i32 121, i32 167, i32 66, i32 20, ; 64..71
	i32 62, i32 145, i32 16, i32 154, i32 61, i32 104, i32 10, i32 2, ; 72..79
	i32 40, i32 162, i32 125, i32 79, i32 142, i32 109, i32 152, i32 42, ; 80..87
	i32 28, i32 131, i32 117, i32 80, i32 127, i32 132, i32 155, i32 51, ; 88..95
	i32 95, i32 39, i32 170, i32 26, i32 54, i32 165, i32 158, i32 125, ; 96..103
	i32 1, i32 113, i32 89, i32 57, i32 2, i32 169, i32 143, i32 29, ; 104..111
	i32 78, i32 21, i32 139, i32 161, i32 6, i32 50, i32 94, i32 36, ; 112..119
	i32 9, i32 111, i32 135, i32 98, i32 92, i32 30, i32 161, i32 33, ; 120..127
	i32 129, i32 144, i32 72, i32 88, i32 151, i32 157, i32 163, i32 84, ; 128..135
	i32 13, i32 6, i32 128, i32 27, i32 99, i32 26, i32 111, i32 152, ; 136..143
	i32 144, i32 118, i32 58, i32 136, i32 71, i32 143, i32 138, i32 81, ; 144..151
	i32 1, i32 147, i32 42, i32 115, i32 151, i32 109, i32 74, i32 59, ; 152..159
	i32 105, i32 33, i32 32, i32 101, i32 141, i32 146, i32 56, i32 166, ; 160..167
	i32 50, i32 168, i32 73, i32 156, i32 30, i32 131, i32 112, i32 115, ; 168..175
	i32 103, i32 123, i32 4, i32 76, i32 49, i32 63, i32 22, i32 3, ; 176..183
	i32 23, i32 171, i32 57, i32 97, i32 120, i32 20, i32 59, i32 179, ; 184..191
	i32 87, i32 172, i32 11, i32 55, i32 175, i32 108, i32 18, i32 132, ; 192..199
	i32 156, i32 92, i32 96, i32 19, i32 106, i32 8, i32 165, i32 40, ; 200..207
	i32 129, i32 160, i32 75, i32 78, i32 8, i32 46, i32 70, i32 137, ; 208..215
	i32 150, i32 119, i32 134, i32 124, i32 52, i32 60, i32 64, i32 89, ; 216..223
	i32 34, i32 124, i32 146, i32 134, i32 130, i32 13, i32 23, i32 21, ; 224..231
	i32 64, i32 37, i32 163, i32 177, i32 14, i32 135, i32 32, i32 77, ; 232..239
	i32 53, i32 72, i32 5, i32 95, i32 107, i32 127, i32 148, i32 24, ; 240..247
	i32 102, i32 10, i32 45, i32 116, i32 149, i32 38, i32 160, i32 17, ; 248..255
	i32 61, i32 9, i32 4, i32 52, i32 69, i32 173, i32 45, i32 94, ; 256..263
	i32 164, i32 147, i32 39, i32 180, i32 84, i32 82, i32 46, i32 179, ; 264..271
	i32 93, i32 173, i32 55, i32 25, i32 81, i32 122, i32 117, i32 48, ; 272..279
	i32 101, i32 51, i32 91, i32 11, i32 119, i32 123, i32 158, i32 34, ; 280..287
	i32 172, i32 44, i32 96, i32 12, i32 174, i32 86, i32 68, i32 82, ; 288..295
	i32 35, i32 141, i32 63, i32 177, i32 170, i32 120, i32 121, i32 114, ; 296..303
	i32 142, i32 77, i32 24, i32 105, i32 74, i32 12, i32 178, i32 67, ; 304..311
	i32 38, i32 83, i32 86, i32 140, i32 159, i32 90, i32 60, i32 43, ; 312..319
	i32 76, i32 15, i32 116, i32 103, i32 56, i32 85, i32 70, i32 113, ; 320..327
	i32 31, i32 48, i32 25, i32 18, i32 41, i32 41, i32 67, i32 0, ; 328..335
	i32 130, i32 122, i32 91, i32 176, i32 133, i32 171, i32 7, i32 118, ; 336..343
	i32 0, i32 175, i32 79, i32 107, i32 149, i32 53, i32 164, i32 97, ; 344..351
	i32 180, i32 27, i32 166, i32 110, i32 7, i32 65, i32 68, i32 90, ; 352..359
	i32 148, i32 110 ; 360..361
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 4; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 4

; Function attributes: "frame-pointer"="all" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 4
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 4
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"min_enum_size", i32 4}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
