## Özellikler
- Rastgele başlayan ve sonsuz dönen playlist.
- Parça geçişinde retro/TV neon flash kodla oynatılır.
- Birden fazla geçiş SFX desteği: listeden rastgele seçilir, aynı SFX art arda tekrarlanmaz.

## MusicManager
- İki AudioSource: `musicSource` (müzik), `sfxSource` (geçiş SFX).
- Inspector alanları:
  - `AudioClip[] tracks`
  - `AudioClip[] transitionSfxList`
  - `float musicVolume = 1f`, `float sfxVolume = 0.8f`
- Akış:
  1. Başlangıçta rastgele bir `trackIndex` ile çal.
  2. Parça bitince:
     - Rastgele bir `transitionSfx` seç (son kullanılanla aynı değil).
     - `sfxSource.PlayOneShot(selectedSfx, sfxVolume)`.
     - Eşzamanlı `RetroFlashEffect.Flash()` başlat.
     - SFX `selectedSfx.length` süresi kadar bekle.
     - Sonraki parçayı seç (öncekiyle aynı değil) ve `musicSource` ile çal.
- Sonsuz döngü ve tekrar engelleme (müzik ve SFX için art arda aynı olmama).

## RetroFlashEffect (tamamen kodla)
- Overlay `Canvas` + çocuk `Image` katmanları: Base Flash, Neon Border (magenta/cyan/purple renk döngüsü), Scanlines (procedural Texture2D), Vignette (procedural radial), Bloom Ring ve CRT jitter.
- Unscaled zamanda coroutine’lerle toplam ~0.5s animasyon.
- Inspector parametreleri ile renk, süre ve yoğunluk ayarlanabilir.

## Uygulama Adımları
1. `MusicManager` scriptini yaz: iki AudioSource, playlist ve SFX akışlarını kur.
2. `RetroFlashEffect` scriptini yaz: overlay ve animasyon katmanlarını programatik oluştur.
3. Sahnede `MusicManager`’ı ekle; `tracks` ve `transitionSfxList`’i doldur.

Onay verirsen bu planla kodları yazıp sahneye entegre edeceğim.