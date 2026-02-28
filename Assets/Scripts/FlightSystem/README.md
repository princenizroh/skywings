# FlightSystem

Ground — fisika saat di darat:

mass — berat pesawat (kg). Makin berat, makin susah berakselerasi
maxThrust — gaya dorong mesin maksimal (Newton). Naikkan kalau pesawat terasa lambat di landasan
brakingForce — kekuatan rem (Newton)
steerSpeed — kecepatan belok roda depan (derajat/detik)
groundDrag — hambatan udara saat di darat. Makin tinggi, makin cepat melambat
groundAngularDrag — hambatan rotasi saat di darat, biar pesawat tidak muter-muter
takeoffSpeed — kecepatan minimum untuk liftoff (km/h)
Ground - Throttle Ramp — seberapa responsif gas:

throttleRampUp — seberapa cepat gas naik saat W ditekan (0–1 per detik)
throttleRampDown — seberapa cepat gas turun saat W dilepas
Physics — setup fisika umum:

centerOfMassY — posisi pusat massa ke bawah. Nilai negatif = lebih stabil, tidak mudah terbalik
Flight - Speed — kontrol kecepatan di udara:

flightAccel — tambah kecepatan saat W ditekan (km/h per detik)
flightBrake — kurang kecepatan saat S ditekan (km/h per detik)
flightIdleDecel — perlambatan idle saat tidak menekan gas (km/h per detik), pesawat mendekati minFlightSpeed
minFlightSpeed — kecepatan minimum agar tetap terbang (km/h). Di bawah ini pesawat akan mendarat
maxFlightSpeed — batas kecepatan maksimum (km/h)
Flight - Control — responsivitas kontrol:

pitchSpeed — kecepatan naik/turun hidung pesawat (derajat/detik), Q/E
rollSpeed — kecepatan miring kiri/kanan (derajat/detik), A/D
bankTurnFactor — seberapa kuat roll otomatis menghasilkan belok (yaw). 0 = tidak ada auto-turn, 1 = sangat agresif
maxPitchAngle — batas maksimal pitch atas/bawah (derajat)
maxRollAngle — batas maksimal kemiringan sayap (derajat)
Flight - Stability — auto-level saat stick dilepas:

autoLevelRoll — kecepatan sayap kembali ke level saat A/D dilepas (derajat/detik)
autoLevelPitch — kecepatan hidung kembali ke level saat Q/E dilepas (derajat/detik)