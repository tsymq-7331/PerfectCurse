from pathlib import Path

from PIL import Image


ROOT = Path(__file__).resolve().parents[1]
GENERATED = (
    Path.home()
    / ".codex"
    / "generated_images"
    / "019ffb1f-da99-7720-9e00-eb356cdf8e81"
)

curse_assets = {
    "plague": ("exec-a153f74c-25ec-428f-8639-eff43de501e1.png", 300),
    "rot": ("exec-b5cc1110-ce36-4ffc-aa1d-dc616ab6ee38.png", 300),
    "oldwound": ("exec-82b7192b-996b-404c-abc0-6161ecd5306e.png", 290),
    "shackles": ("exec-ba0830b5-3ed4-4cc8-8d6f-da643f9ee1ee.png", 260),
    "shortage": ("exec-0a95ad4a-f893-495f-b708-084d6581bdac.png", 570),
}

character_assets = {
    "curseblood": ("exec-491fe64b-193c-40b7-a4f1-75e4e4741c1f.png", 330),
    "starlightaegis": ("exec-e52faa05-e80d-4178-9159-7563a26ce0cb.png", 300),
    "unexpecteddefect": ("exec-e9972718-6213-4d71-892e-5b64c9bec54e.png", 260),
    "ridethepoison": ("exec-e98ea85d-6c32-4a60-a1fb-433819cec637.png", 270),
    "reinforcedsmelting": ("exec-9f72292a-69f4-4bcf-aaa7-b8639fbf6975.png", 300),
}

small_dir = ROOT / "PerfectCurse" / "images" / "card_portraits"
big_dir = small_dir / "big"
small_dir.mkdir(parents=True, exist_ok=True)
big_dir.mkdir(parents=True, exist_ok=True)

for category, assets in (("curses", curse_assets), ("character_cards", character_assets)):
    source_dir = ROOT / "art_source" / category
    source_dir.mkdir(parents=True, exist_ok=True)

    for name, (filename, top) in assets.items():
        source = Image.open(GENERATED / filename).convert("RGB")
        source.save(source_dir / f"{name}.png")

        crop_height = round(source.width * 760 / 1000)
        top = min(max(0, top), source.height - crop_height)
        cropped = source.crop((0, top, source.width, top + crop_height))
        cropped.resize((1000, 760), Image.Resampling.LANCZOS).save(big_dir / f"{name}.png")
        cropped.resize((250, 190), Image.Resampling.LANCZOS).save(small_dir / f"{name}.png")
        print(name, source.size, (0, top, source.width, top + crop_height))
