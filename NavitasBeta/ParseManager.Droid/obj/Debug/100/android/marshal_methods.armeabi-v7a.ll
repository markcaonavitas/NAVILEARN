; ModuleID = 'obj\Debug\100\android\marshal_methods.armeabi-v7a.ll'
source_filename = "obj\Debug\100\android\marshal_methods.armeabi-v7a.ll"
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
@assembly_image_cache_hashes = local_unnamed_addr constant [252 x i32] [
	i32 32687329, ; 0: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 82
	i32 34715100, ; 1: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 111
	i32 39109920, ; 2: Newtonsoft.Json.dll => 0x254c520 => 9
	i32 57263871, ; 3: Xamarin.Forms.Core.dll => 0x369c6ff => 106
	i32 57967248, ; 4: Xamarin.Android.Support.VersionedParcelable.dll => 0x3748290 => 55
	i32 101534019, ; 5: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 98
	i32 120558881, ; 6: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 98
	i32 160529393, ; 7: Xamarin.Android.Arch.Core.Common => 0x9917bf1 => 18
	i32 165246403, ; 8: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 67
	i32 166922606, ; 9: Xamarin.Android.Support.Compat.dll => 0x9f3096e => 29
	i32 182336117, ; 10: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 99
	i32 201930040, ; 11: Xamarin.Android.Arch.Core.Runtime => 0xc093538 => 19
	i32 209399409, ; 12: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 65
	i32 219130465, ; 13: Xamarin.Android.Support.v4 => 0xd0faa61 => 48
	i32 230216969, ; 14: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 77
	i32 232587938, ; 15: Xamarin.AndroidX.MediaRouter => 0xddd02a2 => 88
	i32 232815796, ; 16: System.Web.Services => 0xde07cb4 => 122
	i32 278686392, ; 17: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 81
	i32 280482487, ; 18: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 75
	i32 293914992, ; 19: Xamarin.Android.Support.Transition => 0x1184c970 => 47
	i32 318968648, ; 20: Xamarin.AndroidX.Activity.dll => 0x13031348 => 57
	i32 321597661, ; 21: System.Numerics => 0x132b30dd => 116
	i32 331266987, ; 22: Xamarin.Android.Support.v7.MediaRouter.dll => 0x13bebbab => 51
	i32 342366114, ; 23: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 79
	i32 388313361, ; 24: Xamarin.Android.Support.Animated.Vector.Drawable => 0x17253111 => 25
	i32 389971796, ; 25: Xamarin.Android.Support.Core.UI => 0x173e7f54 => 31
	i32 442565967, ; 26: System.Collections => 0x1a61054f => 124
	i32 450948140, ; 27: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 74
	i32 465846621, ; 28: mscorlib => 0x1bc4415d => 8
	i32 469710990, ; 29: System.dll => 0x1bff388e => 13
	i32 476646585, ; 30: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 75
	i32 486930444, ; 31: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 86
	i32 498788369, ; 32: System.ObjectModel => 0x1dbae811 => 14
	i32 514659665, ; 33: Xamarin.Android.Support.Compat => 0x1ead1551 => 29
	i32 524864063, ; 34: Xamarin.Android.Support.Print => 0x1f48ca3f => 44
	i32 526420162, ; 35: System.Transactions.dll => 0x1f6088c2 => 117
	i32 529739732, ; 36: ParseManager.dll => 0x1f932fd4 => 11
	i32 539750087, ; 37: Xamarin.Android.Support.Design => 0x202beec7 => 36
	i32 571524804, ; 38: Xamarin.Android.Support.v7.RecyclerView => 0x2210c6c4 => 53
	i32 605376203, ; 39: System.IO.Compression.FileSystem => 0x24154ecb => 120
	i32 627609679, ; 40: Xamarin.AndroidX.CustomView => 0x2568904f => 71
	i32 663517072, ; 41: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 103
	i32 666292255, ; 42: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 62
	i32 690569205, ; 43: System.Xml.Linq.dll => 0x29293ff5 => 17
	i32 692692150, ; 44: Xamarin.Android.Support.Annotations => 0x2949a4b6 => 26
	i32 775507847, ; 45: System.IO.Compression => 0x2e394f87 => 119
	i32 801787702, ; 46: Xamarin.Android.Support.Interpolator => 0x2fca4f36 => 40
	i32 809851609, ; 47: System.Drawing.Common.dll => 0x30455ad9 => 113
	i32 843511501, ; 48: Xamarin.AndroidX.Print => 0x3246f6cd => 95
	i32 882883187, ; 49: Xamarin.Android.Support.v4.dll => 0x349fba73 => 48
	i32 890949947, ; 50: ParseManager.Droid => 0x351ad13b => 0
	i32 916714535, ; 51: Xamarin.Android.Support.Print.dll => 0x36a3f427 => 44
	i32 928116545, ; 52: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 111
	i32 955402788, ; 53: Newtonsoft.Json => 0x38f24a24 => 9
	i32 958213972, ; 54: Xamarin.Android.Support.Media.Compat => 0x391d2f54 => 43
	i32 967690846, ; 55: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 79
	i32 974778368, ; 56: FormsViewGroup.dll => 0x3a19f000 => 4
	i32 987342438, ; 57: Xamarin.Android.Arch.Lifecycle.LiveData.dll => 0x3ad9a666 => 22
	i32 992768348, ; 58: System.Collections.dll => 0x3b2c715c => 124
	i32 1012816738, ; 59: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 97
	i32 1035644815, ; 60: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 61
	i32 1042160112, ; 61: Xamarin.Forms.Platform.dll => 0x3e1e19f0 => 108
	i32 1052210849, ; 62: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 83
	i32 1098167829, ; 63: Xamarin.Android.Arch.Lifecycle.ViewModel => 0x4174b615 => 24
	i32 1098259244, ; 64: System => 0x41761b2c => 13
	i32 1175144683, ; 65: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 101
	i32 1178241025, ; 66: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 91
	i32 1204270330, ; 67: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 62
	i32 1267360935, ; 68: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 102
	i32 1292763917, ; 69: Xamarin.Android.Support.CursorAdapter.dll => 0x4d0e030d => 33
	i32 1293217323, ; 70: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 73
	i32 1297413738, ; 71: Xamarin.Android.Arch.Lifecycle.LiveData.Core => 0x4d54f66a => 21
	i32 1359785034, ; 72: Xamarin.Android.Support.Design.dll => 0x510cac4a => 36
	i32 1365406463, ; 73: System.ServiceModel.Internals.dll => 0x516272ff => 112
	i32 1376866003, ; 74: Xamarin.AndroidX.SavedState => 0x52114ed3 => 97
	i32 1379779777, ; 75: System.Resources.ResourceManager => 0x523dc4c1 => 1
	i32 1395857551, ; 76: Xamarin.AndroidX.Media.dll => 0x5333188f => 87
	i32 1406073936, ; 77: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 68
	i32 1445445088, ; 78: Xamarin.Android.Support.Fragment => 0x5627bde0 => 39
	i32 1460219004, ; 79: Xamarin.Forms.Xaml => 0x57092c7c => 109
	i32 1462112819, ; 80: System.IO.Compression.dll => 0x57261233 => 119
	i32 1469204771, ; 81: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 60
	i32 1574652163, ; 82: Xamarin.Android.Support.Core.Utils.dll => 0x5ddb4903 => 32
	i32 1582372066, ; 83: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 72
	i32 1587447679, ; 84: Xamarin.Android.Arch.Core.Common.dll => 0x5e9e877f => 18
	i32 1592978981, ; 85: System.Runtime.Serialization.dll => 0x5ef2ee25 => 3
	i32 1622152042, ; 86: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 85
	i32 1624863272, ; 87: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 105
	i32 1636350590, ; 88: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 70
	i32 1639515021, ; 89: System.Net.Http.dll => 0x61b9038d => 2
	i32 1657153582, ; 90: System.Runtime => 0x62c6282e => 15
	i32 1658251792, ; 91: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 110
	i32 1662014763, ; 92: Xamarin.Android.Support.Vector.Drawable => 0x6310552b => 54
	i32 1729485958, ; 93: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 66
	i32 1746316138, ; 94: Mono.Android.Export => 0x6816ab6a => 7
	i32 1766324549, ; 95: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 99
	i32 1776026572, ; 96: System.Core.dll => 0x69dc03cc => 12
	i32 1788241197, ; 97: Xamarin.AndroidX.Fragment => 0x6a96652d => 74
	i32 1808609942, ; 98: Xamarin.AndroidX.Loader => 0x6bcd3296 => 85
	i32 1813201214, ; 99: Xamarin.Google.Android.Material => 0x6c13413e => 110
	i32 1818569960, ; 100: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 92
	i32 1866717392, ; 101: Xamarin.Android.Support.Interpolator.dll => 0x6f43d8d0 => 40
	i32 1877418711, ; 102: Xamarin.Android.Support.v7.RecyclerView.dll => 0x6fe722d7 => 53
	i32 1878053835, ; 103: Xamarin.Forms.Xaml.dll => 0x6ff0d3cb => 109
	i32 1885316902, ; 104: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 63
	i32 1900610850, ; 105: System.Resources.ResourceManager.dll => 0x71490522 => 1
	i32 1916660109, ; 106: Xamarin.Android.Arch.Lifecycle.ViewModel.dll => 0x723de98d => 24
	i32 1919157823, ; 107: Xamarin.AndroidX.MultiDex.dll => 0x7264063f => 89
	i32 2019465201, ; 108: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 83
	i32 2037417872, ; 109: Xamarin.Android.Support.ViewPager => 0x79708790 => 56
	i32 2044222327, ; 110: Xamarin.Android.Support.Loader => 0x79d85b77 => 41
	i32 2055257422, ; 111: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 80
	i32 2079903147, ; 112: System.Runtime.dll => 0x7bf8cdab => 15
	i32 2097448633, ; 113: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x7d0486b9 => 76
	i32 2126786730, ; 114: Xamarin.Forms.Platform.Android => 0x7ec430aa => 107
	i32 2139458754, ; 115: Xamarin.Android.Support.DrawerLayout => 0x7f858cc2 => 38
	i32 2166116741, ; 116: Xamarin.Android.Support.Core.Utils => 0x811c5185 => 32
	i32 2193016926, ; 117: System.ObjectModel.dll => 0x82b6c85e => 14
	i32 2196165013, ; 118: Xamarin.Android.Support.VersionedParcelable => 0x82e6d195 => 55
	i32 2201231467, ; 119: System.Net.Http => 0x8334206b => 2
	i32 2217644978, ; 120: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 101
	i32 2244775296, ; 121: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 86
	i32 2256548716, ; 122: Xamarin.AndroidX.MultiDex => 0x8680336c => 89
	i32 2261435625, ; 123: Xamarin.AndroidX.Legacy.Support.V4.dll => 0x86cac4e9 => 78
	i32 2279755925, ; 124: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 96
	i32 2315684594, ; 125: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 58
	i32 2330457430, ; 126: Xamarin.Android.Support.Core.UI.dll => 0x8ae7f556 => 31
	i32 2330986192, ; 127: Xamarin.Android.Support.SlidingPaneLayout.dll => 0x8af006d0 => 45
	i32 2373288475, ; 128: Xamarin.Android.Support.Fragment.dll => 0x8d75821b => 39
	i32 2409053734, ; 129: Xamarin.AndroidX.Preference.dll => 0x8f973e26 => 94
	i32 2425099629, ; 130: ParseManager.Droid.dll => 0x908c156d => 0
	i32 2440966767, ; 131: Xamarin.Android.Support.Loader.dll => 0x917e326f => 41
	i32 2471841756, ; 132: netstandard.dll => 0x93554fdc => 114
	i32 2475788418, ; 133: Java.Interop.dll => 0x93918882 => 5
	i32 2487632829, ; 134: Xamarin.Android.Support.DocumentFile => 0x944643bd => 37
	i32 2501346920, ; 135: System.Data.DataSetExtensions => 0x95178668 => 118
	i32 2505896520, ; 136: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 82
	i32 2581819634, ; 137: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 102
	i32 2620871830, ; 138: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 70
	i32 2626405577, ; 139: ParseManager => 0x9c8bc4c9 => 11
	i32 2633051222, ; 140: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 81
	i32 2698266930, ; 141: Xamarin.Android.Arch.Lifecycle.LiveData => 0xa0d44932 => 22
	i32 2715334215, ; 142: System.Threading.Tasks.dll => 0xa1d8b647 => 123
	i32 2732626843, ; 143: Xamarin.AndroidX.Activity => 0xa2e0939b => 57
	i32 2737747696, ; 144: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 60
	i32 2751899777, ; 145: Xamarin.Android.Support.Collections => 0xa406a881 => 28
	i32 2754271178, ; 146: Xamarin.Android.Support.v7.Palette => 0xa42ad7ca => 52
	i32 2766581644, ; 147: Xamarin.Forms.Core => 0xa4e6af8c => 106
	i32 2772484381, ; 148: Xamarin.AndroidX.Palette.dll => 0xa540c11d => 93
	i32 2778768386, ; 149: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 104
	i32 2782647110, ; 150: Xamarin.Android.Support.CustomTabs.dll => 0xa5dbd346 => 34
	i32 2788639665, ; 151: Xamarin.Android.Support.LocalBroadcastManager => 0xa63743b1 => 42
	i32 2788775637, ; 152: Xamarin.Android.Support.SwipeRefreshLayout.dll => 0xa63956d5 => 46
	i32 2810250172, ; 153: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 68
	i32 2819470561, ; 154: System.Xml.dll => 0xa80db4e1 => 16
	i32 2853208004, ; 155: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 104
	i32 2855708567, ; 156: Xamarin.AndroidX.Transition => 0xaa36a797 => 100
	i32 2861098320, ; 157: Mono.Android.Export.dll => 0xaa88e550 => 7
	i32 2876493027, ; 158: Xamarin.Android.Support.SwipeRefreshLayout => 0xab73cce3 => 46
	i32 2893257763, ; 159: Xamarin.Android.Arch.Core.Runtime.dll => 0xac739c23 => 19
	i32 2903344695, ; 160: System.ComponentModel.Composition => 0xad0d8637 => 121
	i32 2905242038, ; 161: mscorlib.dll => 0xad2a79b6 => 8
	i32 2916838712, ; 162: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 105
	i32 2921128767, ; 163: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 59
	i32 2921692953, ; 164: Xamarin.Android.Support.CustomView.dll => 0xae257f19 => 35
	i32 2922925221, ; 165: Xamarin.Android.Support.Vector.Drawable.dll => 0xae384ca5 => 54
	i32 2978675010, ; 166: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 73
	i32 3024354802, ; 167: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 77
	i32 3044182254, ; 168: FormsViewGroup => 0xb57288ee => 4
	i32 3056250942, ; 169: Xamarin.Android.Support.AsyncLayoutInflater.dll => 0xb62ab03e => 27
	i32 3057625584, ; 170: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 90
	i32 3068715062, ; 171: Xamarin.Android.Arch.Lifecycle.Common => 0xb6e8e036 => 20
	i32 3075834255, ; 172: System.Threading.Tasks => 0xb755818f => 123
	i32 3092211740, ; 173: Xamarin.Android.Support.Media.Compat.dll => 0xb84f681c => 43
	i32 3111772706, ; 174: System.Runtime.Serialization => 0xb979e222 => 3
	i32 3191408315, ; 175: Xamarin.Android.Support.CustomTabs => 0xbe3906bb => 34
	i32 3194035187, ; 176: Xamarin.Android.Support.v7.MediaRouter => 0xbe611bf3 => 51
	i32 3204380047, ; 177: System.Data.dll => 0xbefef58f => 115
	i32 3204912593, ; 178: Xamarin.Android.Support.AsyncLayoutInflater => 0xbf0715d1 => 27
	i32 3211777861, ; 179: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 72
	i32 3233339011, ; 180: Xamarin.Android.Arch.Lifecycle.LiveData.Core.dll => 0xc0b8d683 => 21
	i32 3247949154, ; 181: Mono.Security => 0xc197c562 => 125
	i32 3258312781, ; 182: Xamarin.AndroidX.CardView => 0xc235e84d => 66
	i32 3267021929, ; 183: Xamarin.AndroidX.AsyncLayoutInflater => 0xc2bacc69 => 64
	i32 3296380511, ; 184: Xamarin.Android.Support.Collections.dll => 0xc47ac65f => 28
	i32 3317135071, ; 185: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 71
	i32 3317144872, ; 186: System.Data => 0xc5b79d28 => 115
	i32 3321585405, ; 187: Xamarin.Android.Support.DocumentFile.dll => 0xc5fb5efd => 37
	i32 3340431453, ; 188: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 63
	i32 3346324047, ; 189: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 91
	i32 3352662376, ; 190: Xamarin.Android.Support.CoordinaterLayout => 0xc7d59168 => 30
	i32 3353484488, ; 191: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0xc7e21cc8 => 76
	i32 3357663996, ; 192: Xamarin.Android.Support.CursorAdapter => 0xc821e2fc => 33
	i32 3362522851, ; 193: Xamarin.AndroidX.Core => 0xc86c06e3 => 69
	i32 3366347497, ; 194: Java.Interop => 0xc8a662e9 => 5
	i32 3369739654, ; 195: Xamarin.AndroidX.Palette => 0xc8da2586 => 93
	i32 3374999561, ; 196: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 96
	i32 3404865022, ; 197: System.ServiceModel.Internals => 0xcaf21dfe => 112
	i32 3429136800, ; 198: System.Xml => 0xcc6479a0 => 16
	i32 3430777524, ; 199: netstandard => 0xcc7d82b4 => 114
	i32 3439690031, ; 200: Xamarin.Android.Support.Annotations.dll => 0xcd05812f => 26
	i32 3476120550, ; 201: Mono.Android => 0xcf3163e6 => 6
	i32 3486566296, ; 202: System.Transactions => 0xcfd0c798 => 117
	i32 3498942916, ; 203: Xamarin.Android.Support.v7.CardView.dll => 0xd08da1c4 => 50
	i32 3501239056, ; 204: Xamarin.AndroidX.AsyncLayoutInflater.dll => 0xd0b0ab10 => 64
	i32 3509114376, ; 205: System.Xml.Linq => 0xd128d608 => 17
	i32 3536029504, ; 206: Xamarin.Forms.Platform.Android.dll => 0xd2c38740 => 107
	i32 3547625832, ; 207: Xamarin.Android.Support.SlidingPaneLayout => 0xd3747968 => 45
	i32 3567349600, ; 208: System.ComponentModel.Composition.dll => 0xd4a16f60 => 121
	i32 3618140916, ; 209: Xamarin.AndroidX.Preference => 0xd7a872f4 => 94
	i32 3627220390, ; 210: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 95
	i32 3629053394, ; 211: Xamarin.AndroidX.MediaRouter.dll => 0xd84ef5d2 => 88
	i32 3632359727, ; 212: Xamarin.Forms.Platform => 0xd881692f => 108
	i32 3633644679, ; 213: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 59
	i32 3641597786, ; 214: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 80
	i32 3664423555, ; 215: Xamarin.Android.Support.ViewPager.dll => 0xda6aaa83 => 56
	i32 3672681054, ; 216: Mono.Android.dll => 0xdae8aa5e => 6
	i32 3676310014, ; 217: System.Web.Services.dll => 0xdb2009fe => 122
	i32 3678221644, ; 218: Xamarin.Android.Support.v7.AppCompat => 0xdb3d354c => 49
	i32 3681174138, ; 219: Xamarin.Android.Arch.Lifecycle.Common.dll => 0xdb6a427a => 20
	i32 3682565725, ; 220: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 65
	i32 3689375977, ; 221: System.Drawing.Common => 0xdbe768e9 => 113
	i32 3714038699, ; 222: Xamarin.Android.Support.LocalBroadcastManager.dll => 0xdd5fbbab => 42
	i32 3718463572, ; 223: Xamarin.Android.Support.Animated.Vector.Drawable.dll => 0xdda34054 => 25
	i32 3718780102, ; 224: Xamarin.AndroidX.Annotation => 0xdda814c6 => 58
	i32 3724971120, ; 225: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 90
	i32 3758932259, ; 226: Xamarin.AndroidX.Legacy.Support.V4 => 0xe00cc123 => 78
	i32 3776062606, ; 227: Xamarin.Android.Support.DrawerLayout.dll => 0xe112248e => 38
	i32 3786282454, ; 228: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 67
	i32 3789524022, ; 229: Xamarin.Android.Support.v7.Palette.dll => 0xe1df8c36 => 52
	i32 3822602673, ; 230: Xamarin.AndroidX.Media => 0xe3d849b1 => 87
	i32 3829621856, ; 231: System.Numerics.dll => 0xe4436460 => 116
	i32 3832731800, ; 232: Xamarin.Android.Support.CoordinaterLayout.dll => 0xe472d898 => 30
	i32 3862817207, ; 233: Xamarin.Android.Arch.Lifecycle.Runtime.dll => 0xe63de9b7 => 23
	i32 3874897629, ; 234: Xamarin.Android.Arch.Lifecycle.Runtime => 0xe6f63edd => 23
	i32 3883175360, ; 235: Xamarin.Android.Support.v7.AppCompat.dll => 0xe7748dc0 => 49
	i32 3885922214, ; 236: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 100
	i32 3896760992, ; 237: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 69
	i32 3920810846, ; 238: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 120
	i32 3921031405, ; 239: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 103
	i32 3930117279, ; 240: Parse.Android => 0xea40d49f => 10
	i32 3931092270, ; 241: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 92
	i32 3945713374, ; 242: System.Data.DataSetExtensions.dll => 0xeb2ecede => 118
	i32 3955647286, ; 243: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 61
	i32 4063435586, ; 244: Xamarin.Android.Support.CustomView => 0xf2331b42 => 35
	i32 4105002889, ; 245: Mono.Security.dll => 0xf4ad5f89 => 125
	i32 4151237749, ; 246: System.Core => 0xf76edc75 => 12
	i32 4182413190, ; 247: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 84
	i32 4206536145, ; 248: Parse.Android.dll => 0xfabaa5d1 => 10
	i32 4216993138, ; 249: Xamarin.Android.Support.Transition.dll => 0xfb5a3572 => 47
	i32 4219003402, ; 250: Xamarin.Android.Support.v7.CardView => 0xfb78e20a => 50
	i32 4292120959 ; 251: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 84
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [252 x i32] [
	i32 82, i32 111, i32 9, i32 106, i32 55, i32 98, i32 98, i32 18, ; 0..7
	i32 67, i32 29, i32 99, i32 19, i32 65, i32 48, i32 77, i32 88, ; 8..15
	i32 122, i32 81, i32 75, i32 47, i32 57, i32 116, i32 51, i32 79, ; 16..23
	i32 25, i32 31, i32 124, i32 74, i32 8, i32 13, i32 75, i32 86, ; 24..31
	i32 14, i32 29, i32 44, i32 117, i32 11, i32 36, i32 53, i32 120, ; 32..39
	i32 71, i32 103, i32 62, i32 17, i32 26, i32 119, i32 40, i32 113, ; 40..47
	i32 95, i32 48, i32 0, i32 44, i32 111, i32 9, i32 43, i32 79, ; 48..55
	i32 4, i32 22, i32 124, i32 97, i32 61, i32 108, i32 83, i32 24, ; 56..63
	i32 13, i32 101, i32 91, i32 62, i32 102, i32 33, i32 73, i32 21, ; 64..71
	i32 36, i32 112, i32 97, i32 1, i32 87, i32 68, i32 39, i32 109, ; 72..79
	i32 119, i32 60, i32 32, i32 72, i32 18, i32 3, i32 85, i32 105, ; 80..87
	i32 70, i32 2, i32 15, i32 110, i32 54, i32 66, i32 7, i32 99, ; 88..95
	i32 12, i32 74, i32 85, i32 110, i32 92, i32 40, i32 53, i32 109, ; 96..103
	i32 63, i32 1, i32 24, i32 89, i32 83, i32 56, i32 41, i32 80, ; 104..111
	i32 15, i32 76, i32 107, i32 38, i32 32, i32 14, i32 55, i32 2, ; 112..119
	i32 101, i32 86, i32 89, i32 78, i32 96, i32 58, i32 31, i32 45, ; 120..127
	i32 39, i32 94, i32 0, i32 41, i32 114, i32 5, i32 37, i32 118, ; 128..135
	i32 82, i32 102, i32 70, i32 11, i32 81, i32 22, i32 123, i32 57, ; 136..143
	i32 60, i32 28, i32 52, i32 106, i32 93, i32 104, i32 34, i32 42, ; 144..151
	i32 46, i32 68, i32 16, i32 104, i32 100, i32 7, i32 46, i32 19, ; 152..159
	i32 121, i32 8, i32 105, i32 59, i32 35, i32 54, i32 73, i32 77, ; 160..167
	i32 4, i32 27, i32 90, i32 20, i32 123, i32 43, i32 3, i32 34, ; 168..175
	i32 51, i32 115, i32 27, i32 72, i32 21, i32 125, i32 66, i32 64, ; 176..183
	i32 28, i32 71, i32 115, i32 37, i32 63, i32 91, i32 30, i32 76, ; 184..191
	i32 33, i32 69, i32 5, i32 93, i32 96, i32 112, i32 16, i32 114, ; 192..199
	i32 26, i32 6, i32 117, i32 50, i32 64, i32 17, i32 107, i32 45, ; 200..207
	i32 121, i32 94, i32 95, i32 88, i32 108, i32 59, i32 80, i32 56, ; 208..215
	i32 6, i32 122, i32 49, i32 20, i32 65, i32 113, i32 42, i32 25, ; 216..223
	i32 58, i32 90, i32 78, i32 38, i32 67, i32 52, i32 87, i32 116, ; 224..231
	i32 30, i32 23, i32 23, i32 49, i32 100, i32 69, i32 120, i32 103, ; 232..239
	i32 10, i32 92, i32 118, i32 61, i32 35, i32 125, i32 12, i32 84, ; 240..247
	i32 10, i32 47, i32 50, i32 84 ; 248..251
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
