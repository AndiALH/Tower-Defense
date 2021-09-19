# Chapter 4 - Tower Defense

Tugas Chapter 4 dari studi independen Agate Academy bidang game programming. Hal yang dibuat kali ini adalah game Tower Defense.

### Hal-hal yang dimodifikasi / ditambahkan

- Menambahkan konsep elemen pada setiap jenis tower. Untuk kesimpelan, saya membuat tower 1 yang berwarna hijau memiliki elemen angin, tower 2 berwarna merah memiliki elemen api, dan tower 3 berwarna kuning memiliki elemen listrik.
- Memberikan atribut resistance pada enemy terhadap semua elemen tersebut dengan nilai yang berbeda. Dengan nilai resistance tersebut, maka damage yang didapatkan dari elemen tertentu akan dikurangi dengan nilai resistance terhadap elemen tersebut.
- Membuat sistem reaksi elemental terhadap enemy. Secara lebih jelas, enemy akan kena debuff elemen ketika kena serangan dengan jangka waktu tertentu. Jika enemy terkena serangan dari elemen lainnya, maka kedua elemen tersebut akan bereaksi sehingga damage yang diterima enemy akan mendapatkan multiplier tertentu, dan debuff akan hilang. Jika elemen debuff terkena elemen yang sama, maka efek debuff akan diperpanjang.
- Ketika enemy terkena debuff, maka akan ditandakan dengan healthbarnya menjadi berwarna kuning. Jika terkena reaksi elemental, maka darah akan kembali menjadi hijau.
- Memodifikasi nilai-nilai atribut tower dan enemy untuk menyesuaikan kesulitan karena ada mekanisme yang ditambahkan diatas.

Notes:
- Karena saya belum mengimplementasikan feedback yang cukup jelas ketika enemy terkena debuff ataupun reaksi elemental, mungkin jika ingin dipahami lebih jelas bisa melihat debug log saat mengetest gamenya di project.
