using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(GaussianBlurRenderer), PostProcessEvent.AfterStack, "Custom/GaussianBlur")]
public sealed class GaussianBlur : PostProcessEffectSettings
{
    [Range(0f, 5f), Tooltip("Blur size.")]
    public FloatParameter blurSize = new FloatParameter { value = 1f };
}

public sealed class GaussianBlurRenderer : PostProcessEffectRenderer<GaussianBlur>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/GaussianBlur"));
        sheet.properties.SetFloat("_BlurSize", settings.blurSize);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
