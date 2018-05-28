using UnityEngine;

using Zenlytics.Enums;

namespace Zenlytics.Adapters
{

    public abstract class AnalyticsAdapter : ScriptableObject
    {

        /// <summary>
        ///     Business events are used to track in-app purchases with real money.
        /// </summary>
        /// <param name="currency">Currency code in ISO 4217 format. e.g. USD</param>
        /// <param name="amount">Amount in cents. e.g. 99 is 0.99$</param>
        /// <param name="itemType">The type / category of the item. e.g. GoldPacks</param>
        /// <param name="itemId">Specific item bought. e.g. 1000GoldPack</param>
        /// <param name="cartType">The game location of the purchase. Max 10 unique values. e.g. EndOfLevel</param>
        /// <param name="receipt">Android: INAPP_PURCHASE_DATA. iOS: The App Store receipt.</param>
        /// <param name="signature">Android only. INAPP_DATA_SIGNATURE</param>
        public abstract void ReportBusinessEvent(
            string currency,
            int amount,
            string itemType,
            string itemId,
            string cartType,
            string receipt,
            string signature);

        /// <summary>
        ///     Resources events are used to track your in-game economy.
        /// </summary>
        /// <param name="type">Add (source) or subtract (sink) resource.</param>
        /// <param name="currency">One of the available currencies set in the Settings object.</param>
        /// <param name="amount">Amount sourced or sinked.</param>
        /// <param name="itemType">One of the available item types set in the Settings object.</param>
        /// <param name="itemId">Item id. (string max length=32)</param>
        public abstract void ReportResourceEvent(
            ResourceType type,
            string currency,
            float amount,
            string itemType,
            string itemId);

        /// <summary>
        ///     Progression events are used to measure player progression in the game. They follow a 3 tier hierarchy structure
        ///     (world, level and phase) to indicate a player's path or place.
        /// </summary>
        /// <param name="status">Status of added progression (start, complete, fail).</param>
        /// <param name="p01">1st progression (e.g. world01).</param>
        /// <param name="p02">2nd progression (e.g. level01).</param>
        /// <param name="p03">3rd progression (e.g. phase01).</param>
        /// <param name="score">The player's score.</param>
        public abstract void ReportProgressionEvent(
            ProgressionStatus status,
            string p01,
            string p02,
            string p03,
            int score);

        /// <summary>
        ///     Used to track custom error events in the game.
        /// </summary>
        /// <param name="severity">Severity of error (debug, info, warning, error, critical).</param>
        /// <param name="message">Error message</param>
        public abstract void ReportErrorEvent(ErrorSeverity severity, string message);

        /// <summary>
        ///     Used to track any type of design event that you want to measure i.e. GUI elements or tutorial steps.
        /// </summary>
        /// <param name="eventName">
        ///     The event string can have 1 to 5 parts. The parts are separated by ':' with a max length of 64
        ///     each. e.g. "world1:kill:robot:laser".
        /// </param>
        /// <param name="eventValue">Number value of event.</param>
        public abstract void ReportDesignEvent(string eventName, float eventValue);

    }

}