# Struct-Padder
A simple parser used to generate C structs. It parses a structure represented by a similar syntax to the generated C struct.


# example

input:
```
ptrsize=4;

struct Vec3 {
	float x;
	float y;
	float z;
};

struct Player {
	Vec3 Pos1 : 0x24;
	float Pos2[3];
	
	float Health : 0x70;
	float Mana;

	int32 NameLen : 0x84;
	char *Name;
};
```

output:
```
struct Vec3 {
	float x; 	// offs: 0x0
	float y; 	// offs: 0x4
	float z; 	// offs: 0x8
}; // size: 0xC

struct Player {
	char _pad_0x0000[0x24]; 	// offs: 0x0
	Vec3 Pos1; 	// offs: 0x24
	float Pos2[0x3]; 	// offs: 0x30
	char _pad_0x003C[0x34]; 	// offs: 0x3C
	float Health; 	// offs: 0x70
	float Mana; 	// offs: 0x74
	char _pad_0x0078[0xC]; 	// offs: 0x78
	int32 NameLen; 	// offs: 0x84
	char *Name; 	// offs: 0x88
}; // size: 0x8C
```

# directives
## ```ptrsize```
usage: ```ptrsize = 4;```

what it does: It sets the size in bytes of pointers used for calculating offset.


## ```defsize```
usage: ```defsize UnkType = 0x44;```

what it does: It defines a struct stub of the specified size which may be used in structs below.


# member syntax
example: ```Vec3 Pos1 : 0x24;```

what it does: It defines a member called Pos1 with type Vec3 at absolute offset 0x24 within the struct.


example: ```Vec3 Pos2;```

what it does: It defines a member called Pos2 with type Vec3 immediately after the previous member.

# built-in types

Name | Size
--- | ---
```char``` | 1
```__int8``` | 1
```__int16``` | 2
```__int32``` | 4
```__int64``` | 8
```float``` | 4
```double``` | 8
```D3DXVECTOR2``` | 2 * size of float
```D3DXVECTOR3``` | 3 * size of float
```D3DXMATRIX``` | 4 * 4 * size of float
```int8``` | alias to ```__int8```
```int16``` | alias to ```__int16```
```int32``` | alias to ```__int32```
```int64``` | alias to ```__int64```







