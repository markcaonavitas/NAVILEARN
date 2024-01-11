; ModuleID = 'obj\Release\100\android\compressed_assemblies.x86_64.ll'
source_filename = "obj\Release\100\android\compressed_assemblies.x86_64.ll"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-unknown-linux-android"


%struct.CompressedAssemblyDescriptor = type {
	i32,; uint32_t uncompressed_file_size
	i8,; bool loaded
	i8*; uint8_t* data
}

%struct.CompressedAssemblies = type {
	i32,; uint32_t count
	%struct.CompressedAssemblyDescriptor*; CompressedAssemblyDescriptor* descriptors
}
@__CompressedAssemblyDescriptor_data_0 = internal global [15872 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_1 = internal global [166912 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_2 = internal global [81328 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_3 = internal global [1970688 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_4 = internal global [121856 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_5 = internal global [684544 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_6 = internal global [235008 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_7 = internal global [388096 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_8 = internal global [7168 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_9 = internal global [400384 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_10 = internal global [747520 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_11 = internal global [26112 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_12 = internal global [212480 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_13 = internal global [38912 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_14 = internal global [419328 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_15 = internal global [55808 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_16 = internal global [65024 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_17 = internal global [1397760 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_18 = internal global [892928 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_19 = internal global [51712 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_20 = internal global [15872 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_21 = internal global [459776 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_22 = internal global [17408 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_23 = internal global [78848 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_24 = internal global [530944 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_25 = internal global [8704 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_26 = internal global [43520 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_27 = internal global [174080 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_28 = internal global [15360 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_29 = internal global [14848 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_30 = internal global [15872 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_31 = internal global [16896 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_32 = internal global [36352 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_33 = internal global [411136 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_34 = internal global [12800 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_35 = internal global [39936 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_36 = internal global [57344 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_37 = internal global [1207296 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_38 = internal global [863232 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_39 = internal global [191368 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_40 = internal global [103424 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_41 = internal global [232960 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_42 = internal global [18072 x i8] zeroinitializer, align 16
@__CompressedAssemblyDescriptor_data_43 = internal global [2121728 x i8] zeroinitializer, align 16


; Compressed assembly data storage
@compressed_assembly_descriptors = internal global [44 x %struct.CompressedAssemblyDescriptor] [
	; 0
	%struct.CompressedAssemblyDescriptor {
		i32 15872, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15872 x i8], [15872 x i8]* @__CompressedAssemblyDescriptor_data_0, i32 0, i32 0); data
	}, 
	; 1
	%struct.CompressedAssemblyDescriptor {
		i32 166912, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([166912 x i8], [166912 x i8]* @__CompressedAssemblyDescriptor_data_1, i32 0, i32 0); data
	}, 
	; 2
	%struct.CompressedAssemblyDescriptor {
		i32 81328, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([81328 x i8], [81328 x i8]* @__CompressedAssemblyDescriptor_data_2, i32 0, i32 0); data
	}, 
	; 3
	%struct.CompressedAssemblyDescriptor {
		i32 1970688, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1970688 x i8], [1970688 x i8]* @__CompressedAssemblyDescriptor_data_3, i32 0, i32 0); data
	}, 
	; 4
	%struct.CompressedAssemblyDescriptor {
		i32 121856, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([121856 x i8], [121856 x i8]* @__CompressedAssemblyDescriptor_data_4, i32 0, i32 0); data
	}, 
	; 5
	%struct.CompressedAssemblyDescriptor {
		i32 684544, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([684544 x i8], [684544 x i8]* @__CompressedAssemblyDescriptor_data_5, i32 0, i32 0); data
	}, 
	; 6
	%struct.CompressedAssemblyDescriptor {
		i32 235008, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([235008 x i8], [235008 x i8]* @__CompressedAssemblyDescriptor_data_6, i32 0, i32 0); data
	}, 
	; 7
	%struct.CompressedAssemblyDescriptor {
		i32 388096, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([388096 x i8], [388096 x i8]* @__CompressedAssemblyDescriptor_data_7, i32 0, i32 0); data
	}, 
	; 8
	%struct.CompressedAssemblyDescriptor {
		i32 7168, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([7168 x i8], [7168 x i8]* @__CompressedAssemblyDescriptor_data_8, i32 0, i32 0); data
	}, 
	; 9
	%struct.CompressedAssemblyDescriptor {
		i32 400384, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([400384 x i8], [400384 x i8]* @__CompressedAssemblyDescriptor_data_9, i32 0, i32 0); data
	}, 
	; 10
	%struct.CompressedAssemblyDescriptor {
		i32 747520, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([747520 x i8], [747520 x i8]* @__CompressedAssemblyDescriptor_data_10, i32 0, i32 0); data
	}, 
	; 11
	%struct.CompressedAssemblyDescriptor {
		i32 26112, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([26112 x i8], [26112 x i8]* @__CompressedAssemblyDescriptor_data_11, i32 0, i32 0); data
	}, 
	; 12
	%struct.CompressedAssemblyDescriptor {
		i32 212480, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([212480 x i8], [212480 x i8]* @__CompressedAssemblyDescriptor_data_12, i32 0, i32 0); data
	}, 
	; 13
	%struct.CompressedAssemblyDescriptor {
		i32 38912, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([38912 x i8], [38912 x i8]* @__CompressedAssemblyDescriptor_data_13, i32 0, i32 0); data
	}, 
	; 14
	%struct.CompressedAssemblyDescriptor {
		i32 419328, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([419328 x i8], [419328 x i8]* @__CompressedAssemblyDescriptor_data_14, i32 0, i32 0); data
	}, 
	; 15
	%struct.CompressedAssemblyDescriptor {
		i32 55808, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([55808 x i8], [55808 x i8]* @__CompressedAssemblyDescriptor_data_15, i32 0, i32 0); data
	}, 
	; 16
	%struct.CompressedAssemblyDescriptor {
		i32 65024, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([65024 x i8], [65024 x i8]* @__CompressedAssemblyDescriptor_data_16, i32 0, i32 0); data
	}, 
	; 17
	%struct.CompressedAssemblyDescriptor {
		i32 1397760, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1397760 x i8], [1397760 x i8]* @__CompressedAssemblyDescriptor_data_17, i32 0, i32 0); data
	}, 
	; 18
	%struct.CompressedAssemblyDescriptor {
		i32 892928, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([892928 x i8], [892928 x i8]* @__CompressedAssemblyDescriptor_data_18, i32 0, i32 0); data
	}, 
	; 19
	%struct.CompressedAssemblyDescriptor {
		i32 51712, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([51712 x i8], [51712 x i8]* @__CompressedAssemblyDescriptor_data_19, i32 0, i32 0); data
	}, 
	; 20
	%struct.CompressedAssemblyDescriptor {
		i32 15872, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15872 x i8], [15872 x i8]* @__CompressedAssemblyDescriptor_data_20, i32 0, i32 0); data
	}, 
	; 21
	%struct.CompressedAssemblyDescriptor {
		i32 459776, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([459776 x i8], [459776 x i8]* @__CompressedAssemblyDescriptor_data_21, i32 0, i32 0); data
	}, 
	; 22
	%struct.CompressedAssemblyDescriptor {
		i32 17408, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([17408 x i8], [17408 x i8]* @__CompressedAssemblyDescriptor_data_22, i32 0, i32 0); data
	}, 
	; 23
	%struct.CompressedAssemblyDescriptor {
		i32 78848, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([78848 x i8], [78848 x i8]* @__CompressedAssemblyDescriptor_data_23, i32 0, i32 0); data
	}, 
	; 24
	%struct.CompressedAssemblyDescriptor {
		i32 530944, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([530944 x i8], [530944 x i8]* @__CompressedAssemblyDescriptor_data_24, i32 0, i32 0); data
	}, 
	; 25
	%struct.CompressedAssemblyDescriptor {
		i32 8704, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([8704 x i8], [8704 x i8]* @__CompressedAssemblyDescriptor_data_25, i32 0, i32 0); data
	}, 
	; 26
	%struct.CompressedAssemblyDescriptor {
		i32 43520, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([43520 x i8], [43520 x i8]* @__CompressedAssemblyDescriptor_data_26, i32 0, i32 0); data
	}, 
	; 27
	%struct.CompressedAssemblyDescriptor {
		i32 174080, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([174080 x i8], [174080 x i8]* @__CompressedAssemblyDescriptor_data_27, i32 0, i32 0); data
	}, 
	; 28
	%struct.CompressedAssemblyDescriptor {
		i32 15360, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15360 x i8], [15360 x i8]* @__CompressedAssemblyDescriptor_data_28, i32 0, i32 0); data
	}, 
	; 29
	%struct.CompressedAssemblyDescriptor {
		i32 14848, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([14848 x i8], [14848 x i8]* @__CompressedAssemblyDescriptor_data_29, i32 0, i32 0); data
	}, 
	; 30
	%struct.CompressedAssemblyDescriptor {
		i32 15872, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([15872 x i8], [15872 x i8]* @__CompressedAssemblyDescriptor_data_30, i32 0, i32 0); data
	}, 
	; 31
	%struct.CompressedAssemblyDescriptor {
		i32 16896, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([16896 x i8], [16896 x i8]* @__CompressedAssemblyDescriptor_data_31, i32 0, i32 0); data
	}, 
	; 32
	%struct.CompressedAssemblyDescriptor {
		i32 36352, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([36352 x i8], [36352 x i8]* @__CompressedAssemblyDescriptor_data_32, i32 0, i32 0); data
	}, 
	; 33
	%struct.CompressedAssemblyDescriptor {
		i32 411136, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([411136 x i8], [411136 x i8]* @__CompressedAssemblyDescriptor_data_33, i32 0, i32 0); data
	}, 
	; 34
	%struct.CompressedAssemblyDescriptor {
		i32 12800, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([12800 x i8], [12800 x i8]* @__CompressedAssemblyDescriptor_data_34, i32 0, i32 0); data
	}, 
	; 35
	%struct.CompressedAssemblyDescriptor {
		i32 39936, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([39936 x i8], [39936 x i8]* @__CompressedAssemblyDescriptor_data_35, i32 0, i32 0); data
	}, 
	; 36
	%struct.CompressedAssemblyDescriptor {
		i32 57344, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([57344 x i8], [57344 x i8]* @__CompressedAssemblyDescriptor_data_36, i32 0, i32 0); data
	}, 
	; 37
	%struct.CompressedAssemblyDescriptor {
		i32 1207296, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([1207296 x i8], [1207296 x i8]* @__CompressedAssemblyDescriptor_data_37, i32 0, i32 0); data
	}, 
	; 38
	%struct.CompressedAssemblyDescriptor {
		i32 863232, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([863232 x i8], [863232 x i8]* @__CompressedAssemblyDescriptor_data_38, i32 0, i32 0); data
	}, 
	; 39
	%struct.CompressedAssemblyDescriptor {
		i32 191368, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([191368 x i8], [191368 x i8]* @__CompressedAssemblyDescriptor_data_39, i32 0, i32 0); data
	}, 
	; 40
	%struct.CompressedAssemblyDescriptor {
		i32 103424, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([103424 x i8], [103424 x i8]* @__CompressedAssemblyDescriptor_data_40, i32 0, i32 0); data
	}, 
	; 41
	%struct.CompressedAssemblyDescriptor {
		i32 232960, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([232960 x i8], [232960 x i8]* @__CompressedAssemblyDescriptor_data_41, i32 0, i32 0); data
	}, 
	; 42
	%struct.CompressedAssemblyDescriptor {
		i32 18072, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([18072 x i8], [18072 x i8]* @__CompressedAssemblyDescriptor_data_42, i32 0, i32 0); data
	}, 
	; 43
	%struct.CompressedAssemblyDescriptor {
		i32 2121728, ; uncompressed_file_size
		i8 0, ; loaded
		i8* getelementptr inbounds ([2121728 x i8], [2121728 x i8]* @__CompressedAssemblyDescriptor_data_43, i32 0, i32 0); data
	}
], align 16; end of 'compressed_assembly_descriptors' array


; compressed_assemblies
@compressed_assemblies = local_unnamed_addr global %struct.CompressedAssemblies {
	i32 44, ; count
	%struct.CompressedAssemblyDescriptor* getelementptr inbounds ([44 x %struct.CompressedAssemblyDescriptor], [44 x %struct.CompressedAssemblyDescriptor]* @compressed_assembly_descriptors, i32 0, i32 0); descriptors
}, align 8


!llvm.module.flags = !{!0, !1}
!llvm.ident = !{!2}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
