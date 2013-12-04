using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Chess
{
    class Camera
    {
        public int sizeOfTile { get; private set; }
        public int borderSize { get; private set; }

        public Camera()
        {
            sizeOfTile = 64;
            borderSize = 64;
        }

        //Räknar om kordinater för fönstret utan att skala om (Ingen marginal vilket förvränger
        //formen om t.ex. bredden är större än höjden)
        public KeyValuePair<int, int> getVisualCordinatesNewScale(int width, int height, int xX, int yY)
        {
            int sizeOfTileX = width / 10;
            int sizeOfTileY = height / 10;

            int visualX = sizeOfTileX + (xX * sizeOfTileX);
            int visualY = sizeOfTileY + (yY * sizeOfTileY);

            return new KeyValuePair<int, int>(visualX, visualY);
        }

        //Returnerar visuella kordinater och räknar om så chackspelplanen behåller sin form 
        //(skapar marginaler för top+botten eller vänster+höger beroende på landskap eller
        //porträttläge för fönstret
        public KeyValuePair<int, int> getVisualCordinatesNewScaleCeepRatio(int width, int height, 
                                                                        int xX, int yY, bool white = true)
        {
            int sizeOfTileScaled = 0;
            int diffX = 0;
            int diffY = 0;

            //Sätter den höjden och bredden till vara samma storlek (Som den minsta av de två)
            //Därefter delas resten som blir kvar i två och dessa bitar är marginalen för top+botten
            //eller vänster+höger 
            if (width < height)
            {
                //Marginalen
                diffY = (height - width) / 2;
                //Rutstorlek
                sizeOfTileScaled = width / 10;
            }
            else if (width > height)
            {
                //Marginalens
                diffX = (width - height) / 2;
                //Rutstorlek
                sizeOfTileScaled = height / 10;
            }
            else
                sizeOfTileScaled = height / 10;

            int visualX;
            int visualY;

            //För normal uträkning (Top till botten, vänster till höger)
            if (white)
            {
                visualX = diffX + sizeOfTileScaled + (xX * sizeOfTileScaled);
                visualY = diffY + sizeOfTileScaled + (yY * sizeOfTileScaled);
            }
            //För omvänd uträkning (Botten till toppen, höger till vänster)
            else
            {
                visualX = diffX + sizeOfTileScaled + ((7 - xX) * sizeOfTileScaled);
                visualY = diffY + sizeOfTileScaled + ((7 - yY) * sizeOfTileScaled);
            }

            return new KeyValuePair<int, int>(visualX, visualY);
        }

        //Returnerar visuella kordinater (Genom att multiplicera possition med visuell bredd)
        public KeyValuePair<int, int> getVisualCordinatesWhite(int xX, int yY)
        {
            int visualX = borderSize + (xX * sizeOfTile);
            int visualY = borderSize + (yY * sizeOfTile);

            return new KeyValuePair<int, int>(visualX, visualY);
        }

        //Omvandlar spelplanen till svarts synvinkel (Vrider spelplanen)
        public KeyValuePair<int, int> getVisualCordinatesBlack(int xX, int yY)
        {
            //7 är för antalet chackrutor (minus en) från vänster. 
            //D.v.s. att uträkningen blir borderSize + bort till vänstra sidan av rutan som 
            //som den logiska X-kordinaten gäller - antalet rutor i X-led * sizeOfTile
            int visualX = borderSize + ((7 - xX) * sizeOfTile);
            //7 är för antalet chackrutor (minus en) från toppen. 
            //D.v.s. att uträkningen blir borderSize + ner till toppen av rutan som 
            //som den logiska Y-kordinaten gäller - antalet rutor i Y-led * sizeOfTile
            int visualY = borderSize + ((7 - yY) * sizeOfTile);

            return new KeyValuePair<int, int>(visualX, visualY);
        }
    }
}
