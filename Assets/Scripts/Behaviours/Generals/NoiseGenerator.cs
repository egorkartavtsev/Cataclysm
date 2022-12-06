using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using GameData;
using Models;

public class NoiseGenerator : MonoBehaviour
{
    [SerializeField] private Texture2D _texture;
    [SerializeField] int size = 1500;
    List<Tile> Tiles = new List<Tile>();
    System.Random _rand = new System.Random();

    public WorldResourceShop resourceShop;

    [Header("GenerateSettings")]
    [SerializeField] private int WorldSeed = 2;
    [SerializeField] private float scale = 140f;
    [SerializeField] private int octaves = 3;
    [SerializeField] private float persistence = 3f;
    [SerializeField] private float lacunarity = 0.1f;

    public void StartNewWorld()
    {
        _texture = new Texture2D(size, size);
        GetComponent<Renderer>().material.mainTexture = _texture;
        createWorld();
        WorldData.WorldMapTexture = _texture.EncodeToJPG();
        GenerateLocations();
        ResourceAllocation();
        SettlementAllocation();
        GeneratePlayer();
        WorldData.SaveWorldData();
        ChangeScene();
    }

    public void LoadingWorld()
    {
        _texture = new Texture2D(size, size);
        //GetComponent<Renderer>().material.mainTexture = _texture;
        WorldData.LoadWorldData();
        _texture.LoadImage(WorldData.WorldMapTexture, true);
        ChangeScene();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Location", LoadSceneMode.Single);
    }

    #region GenerateWorld.Tiles
    public void createWorld()
    {
        System.Random rnd = new System.Random();
        WorldSeed = rnd.Next(-10000, 10000);
        Tiles = new List<Tile>();
        long st = DateTime.Now.Ticks;

        // Порождающий элемент
        System.Random rand = new System.Random(WorldSeed);

        // Сдвиг октав, чтобы при наложении друг на друга получить более интересную картинку
        Vector2[] octavesOffset = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            // Учитываем внешний сдвиг положения
            float xOffset = rand.Next(-100000, 100000);
            float yOffset = rand.Next(-100000, 100000);
            octavesOffset[i] = new Vector2(xOffset / size, yOffset / size);
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        // Учитываем половину ширины и высоты, для более визуально приятного изменения масштаба
        float halfWidth = size / 2f;
        float halfHeight = size / 2f;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                // Задаём значения для первой октавы
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                float superpositionCompensation = 0;

                // Обработка наложения октав
                for (int i = 0; i < octaves; i++)
                {
                    // Рассчитываем координаты для получения значения из Шума Перлина
                    float xResult = (x - halfWidth) / scale * frequency + octavesOffset[i].x * frequency;
                    float yResult = (y - halfHeight) / scale * frequency + octavesOffset[i].y * frequency;

                    // Получение высоты из ГСПЧ
                    float generatedValue = Mathf.PerlinNoise(xResult, yResult);
                    // Наложение октав
                    noiseHeight += generatedValue * amplitude;
                    // Компенсируем наложение октав, чтобы остаться в границах диапазона [0,1]
                    noiseHeight -= superpositionCompensation;

                    // Расчёт амплитуды, частоты и компенсации для следующей октавы
                    amplitude *= persistence;
                    frequency *= lacunarity;
                    superpositionCompensation = amplitude / 2;
                }

                // Сохраняем точку для карты высот
                // Из-за наложения октав есть вероятность выхода за границы диапазона [0,1]
                //noiseMap[y * MapWidth + x] = PostProcess(Mathf.Clamp01(noiseHeight));

                Tile curTile = new Tile()
                {
                    X = x,
                    Z = y,
                    Background = 0,
                    GlobalId = y*size + x
                };

                DrawInWorldMap(curTile, Mathf.Clamp01(noiseHeight));
                Tiles.Add(curTile);
            }
        }

        if (CheckWaterCount())
            createWorld();
        else
        {
            _texture.Apply();
        }

    }

    void GenerateLocations()
    {
        long st = DateTime.Now.Ticks;
        System.Random rand = new System.Random();
        //Генерируем 16 локаций
        for (int i = 0; i < 16; i++)
        {
            LocationData location = new LocationData();
            location.SetName($"Zone{i + 1}");
            Tile needly = new Tile();
            do
            {
                needly = Tiles[rand.Next(0, Tiles.Where<Tile>(t => !t.Location).ToList().Count)];
            }
            while (!CheckTileZone(needly, location));
            WorldData.AddLocation(location);
        }
    }

    bool CheckTileZone(Tile tile, LocationData location)
    {
        List<Tile> checkedTiles = GetTileZone(tile, 90);

        bool res = false;

        int water = checkedTiles.Where<Tile>(t => t.Background == BackgroundType.Water).ToList().Count;
        int exPercent = (water * 100) / checkedTiles.Count;

        if (checkedTiles.Count == 32761 && exPercent < 35)
        {
            res = true;
            location.AddTiles(GetTileZone(tile, 75));

            GetTileZone(tile, 75)
            .ForEach(t =>
                {
                    t.Location = true;
                }
            );
            _texture.Apply();
        }
        return res;
    }

    bool CheckWaterCount()
    {
        int water = Tiles.Where<Tile>(t => t.Background == BackgroundType.Water).ToList().Count;
        int exPercent = (water * 100) / Tiles.Count;

        return exPercent > 35 || exPercent < 15;
    }

    List<Tile> GetTileZone(Tile center, int radius)
    {
        return Tiles
            .Where<Tile>(t =>
                t.X >= center.X - radius &&
                t.X <= center.X + radius &&
                t.Z >= center.Z - radius &&
                t.Z <= center.Z + radius &&
                !t.Location
            ).ToList();
    }

    void DrawInWorldMap(Tile tile, float noise)
    {
        Color col = Color.black;

        if (noise > 0.1f)
        {
            tile.Background = BackgroundType.Ground;
            col = Color.green;
        }
        else if (noise > 0.02f)
        {
            tile.Background = BackgroundType.Sand;
            col = Color.yellow;
        }
        else
        {
            tile.Background = BackgroundType.Water;
            col = Color.blue;
        }

        _texture.SetPixel(size - tile.X, tile.Z, col);
    }
    #endregion

    #region GenerateWorld.Resources

    void ResourceAllocation()
    {
        foreach (WRSItem res in resourceShop.Shop.Where<WRSItem>(item => item.SO.ItemType == ResItemType.Resource).ToList())
        {
            switch (res.SO.RarityType)
            {
                case ResRarityType.Common:
                    AllocateCommonRes(res);
                    break;
                case ResRarityType.Rare:
                    AllocateRareRes(res);
                    break;
                case ResRarityType.Unique:
                    AllocateUniqueRes(res);
                    break;
            }
        }
    }

    void AllocateCommonRes(WRSItem item)
    {
        int cntPerLocation = Mathf.RoundToInt(item.AvailableCount / WorldData.Locations.Count);
        foreach (LocationData location in WorldData.Locations)
        {
            location.AddResItem(item.SO.Name, cntPerLocation);
        }

    }
    void AllocateRareRes(WRSItem item)
    {
        int locCount = _rand.Next(2, 4);
        int cntPerLocation = Mathf.RoundToInt(item.AvailableCount / locCount);
        for (int i = 0; i < locCount; i++)
        {
            int ind = _rand.Next(0, WorldData.Locations.Count - 1);
            WorldData.Locations[ind].AddResItem(item.SO.Name, cntPerLocation);
        }

    }
    void AllocateUniqueRes(WRSItem item)
    {
        int ind = _rand.Next(0, WorldData.Locations.Count - 1);
        WorldData.Locations[ind].AddResItem(item.SO.Name, item.AvailableCount);
    }

    #endregion

    #region GenerateWorld.Settlements

    void SettlementAllocation()
    {
        if (WorldData.Settlements == null)
            WorldData.Settlements = new List<SettlementData>();

        //устанавливаем поселение игрока        
        WorldData
            .Locations[_rand.Next(0, WorldData.Locations.Count - 1)]
            .SetSettlement(true);

        for (int i = 1; i < 8; i++)
        {
            WorldData
                .Locations
                .Where<LocationData>(
                    l =>
                        l.Settlement == null
                )
                .ToList()[_rand.Next(0, (WorldData.Locations.Count - i) - 1)]
                .SetSettlement(false);
        }
    }

    void GeneratePlayer()
    {
        Character player = new Character(
            0,
            "Player Name",
            100,
            WorldData.Locations.Find(l => l.Current).Name,
            WorldData.Settlements.Find(s => s.Home)
        );
        WorldData.Characters = new List<Character>();
        WorldData.Characters.Add(player);
    }

    #endregion

    #region Debug

    #endregion
}