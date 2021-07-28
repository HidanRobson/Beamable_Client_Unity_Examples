﻿using UnityEngine;
using System.Threading.Tasks;
using Beamable.Common.Inventory;
using Beamable.UI.Scripts;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Beamable.Examples.Services.CommerceService
{
    /// <summary>
    /// Store commonly reused functionality for concerns: General
    /// </summary>
    public static class CommerceServiceHelper
    {
        //  Other Methods --------------------------------
        
        /// <summary>
        /// Load full details of a Beamable Item
        /// </summary>
        /// <param name="_beamableAPI"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<ItemContent> GetItemContentById(IBeamableAPI _beamableAPI, string id)
        {
            return await _beamableAPI.ContentService.GetContent(id, typeof(ItemContent)) as ItemContent;
        }
        
        /// <summary>
        /// Load full details of a Beamable Currency
        /// </summary>
        /// <param name="_beamableAPI"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<CurrencyContent> GetCurrencyContentById(IBeamableAPI _beamableAPI, string id)
        {
            return await _beamableAPI.ContentService.GetContent(id, typeof(CurrencyContent)) as CurrencyContent;
        }

        /// <summary>
        /// Lazily load a AssetReferenceTexture2D into an Image
        /// </summary>
        /// <param name="assetReferenceTexture2D"></param>
        /// <param name="destinationImage"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddressablesLoadAssetAsync<T>(AssetReferenceTexture2D assetReferenceTexture2D, Image destinationImage)
        {
            AsyncOperationHandle<Texture2D> asyncOperationHandle1 = Addressables.LoadAssetAsync<Texture2D>(
               assetReferenceTexture2D);

            asyncOperationHandle1.Completed += (AsyncOperationHandle<Texture2D> asyncOperationHandle2) =>
            {
                Texture2D texture2D = asyncOperationHandle2.Result;
                destinationImage.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height),
                new Vector2(0.5f, 0.5f));

            };
        }

        /// <summary>
        /// Lazily Load an addressable <see cref="Sprite"/> into a UI <see cref="Image"/>.
        /// </summary>
        /// <param name="assetReferenceSprite"></param>
        /// <param name="destinationImage"></param>
        /// <typeparam name="T"></typeparam>
        public static async void AddressablesLoadAssetAsync<T>(AssetReferenceSprite assetReferenceSprite, Image destinationImage)
        {
            // Check before await
            if (destinationImage == null || assetReferenceSprite == null)
            {
                return;
            }

            // Hide it
            Sprite sprite = await AddressableSpriteLoader.LoadSprite(assetReferenceSprite);

            // Check after await
            if (destinationImage == null || assetReferenceSprite == null)
            {
                return;
            }

            if (sprite != null)
            {
                destinationImage.sprite = sprite;
                destinationImage.preserveAspect = true;
            }
        }
    }
}