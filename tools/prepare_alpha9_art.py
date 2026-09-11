from pathlib import Path

from PIL import Image


ROOT = Path(__file__).resolve().parents[1]
GENERATED = Path.home() / ".codex" / "generated_images" / "019ffb1f-da99-7720-9e00-eb356cdf8e81"
ASSETS = {
    "theft": "exec-2f3a9c1c-5bdb-42ef-accc-e852a99762ce.png",
    "parasite": "exec-527f73b5-9a9a-4ecf-8d48-33d3c6d6df57.png",
    "burningalive": "exec-4f96f9c0-8809-4c85-9d90-a8c019cb9a79.png",
    "betrayal": "exec-65188dab-7275-4c77-8edd-44406c9587a4.png",
    "blank": "exec-f0d3566d-29ea-4f03-b1cd-a4ee8910e23a.png",
    "indoctrination": "exec-e25be354-effd-4a67-bb49-da57c332ddbf.png",
}

source_dir = ROOT / "art_source" / "curses"
small_dir = ROOT / "PerfectCurse" / "images" / "card_portraits"
big_dir = small_dir / "big"
source_dir.mkdir(parents=True, exist_ok=True)
small_dir.mkdir(parents=True, exist_ok=True)
big_dir.mkdir(parents=True, exist_ok=True)

for name, filename in ASSETS.items():
    source = Image.open(GENERATED / filename).convert("RGB")
    source.save(source_dir / f"{name}.png")

    target_ratio = 1000 / 760
    source_ratio = source.width / source.height
    if source_ratio > target_ratio:
        crop_width = round(source.height * target_ratio)
        left = (source.width - crop_width) // 2
        crop = (left, 0, left + crop_width, source.height)
    else:
        crop_height = round(source.width / target_ratio)
        top = (source.height - crop_height) // 2
        crop = (0, top, source.width, top + crop_height)

    portrait = source.crop(crop)
    portrait.resize((1000, 760), Image.Resampling.LANCZOS).save(big_dir / f"{name}.png")
    portrait.resize((250, 190), Image.Resampling.LANCZOS).save(small_dir / f"{name}.png")
    print(name, source.size, crop)
