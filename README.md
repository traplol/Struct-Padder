# Struct-Padder
A simple struct parser to automatically pad based on fixed offsets.


example input:

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
