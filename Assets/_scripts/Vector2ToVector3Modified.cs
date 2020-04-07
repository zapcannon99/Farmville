namespace Zinnia.Data.Type.Transformation.Conversion {
    using UnityEngine;
    using UnityEngine.Events;
    using System;
    using Malimbe.XmlDocumentationAttribute;
    using Malimbe.PropertySerializationAttribute;

//using Zinnia.Data.Type.Transformation;
//using Zinnia.Data.Type.Transformation.Conversion;
    /// <summary>
    /// Transforms a <see cref="Vector2"/> to a <see cref="Vector3"/> and allows mapping of the relevant coordinates.
    /// </summary>
    /// <example>
    /// Vector2(1f, 2f) -> XToXAndYToY -> Vector3(1f, 2f, 0f)
    /// Vector2(1f, 2f) -> XToXAndYToZ -> Vector3(1f, 0f, 2f)
    /// Vector2(1f, 2f) -> XToYAndYToX -> Vector3(2f, 1f, 0f)
    /// </example>
    public class Vector2ToVector3Modified : Transformer<Vector2, Vector3, Vector2ToVector3.UnityEvent> {
        /// <summary>
        /// Defines the event with the transformed <see cref="Vector3"/> value.
        /// </summary>
        [Serializable]
        public class UnityEvent : UnityEvent<Vector3> {
        }

        /// <summary>
        /// The mapping of <see cref="Vector2"/> coordinates to the <see cref="Vector3"/> coordinates.
        /// </summary>
        public enum CoordinateMapType {
            /// <summary>
            /// Maps (X,Y) to (X,Y,-)
            /// </summary>
            XToXAndYToY,
            /// <summary>
            /// Maps (X,Y) to (X,-,Y)
            /// </summary>
            XToXAndYToZ,
            /// <summary>
            /// Maps (X,Y) to (Y,X,-)
            /// </summary>
            XToYAndYToX,
            /// <summary>
            /// Maps (X,Y) to (-,X,Y)
            /// </summary>
            XToYAndYToZ,
            /// <summary>
            /// Maps (X,Y) to (Y,-,X)
            /// </summary>
            XToZAndYToX,
            /// <summary>
            /// Maps (X,Y) to (-,Y,X)
            /// </summary>
            XToZAndYToY
        }

        /// <summary>
        /// The mechanism for mapping the <see cref="Vector2"/> coordinates to the <see cref="Vector3"/> coordinates.
        /// </summary>
        [Serialized]
        [field: DocumentedByXml]
        public CoordinateMapType CoordinateMap { get; set; } = CoordinateMapType.XToXAndYToZ;

        /// <summary>
        /// The value to set the unused coordinate to during the conversion.
        /// </summary>
        [Serialized]
        [field: DocumentedByXml]
        public float UnusedCoordinateValue { get; set; } = 0;

        /// <summary>
        /// Transforms the given <see cref="Vector2"/> into a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="input">The value to transform.</param>
        /// <returns>The transformed value.</returns>
        protected override Vector3 Process(Vector2 input) {
            //////////////////////////////////////////////
            // FSM Movement Controller
            //////////////////////////////////////////////

            float movementModifier = 1f;

            // This section is to help with pressing all the buttons
            // Crawling is lowest priority
            if(Input.GetButton("Crawl")) {
                movementModifier = MovementModifierController.CrawlingMovementModifier;
            }

            // Running input takes priority over crouching, so this is the first check
            if(Input.GetButton("Crouch")) {
                movementModifier = MovementModifierController.CrouchingMovementModifier;
            }

            if(Input.GetButton("Run")) {
                movementModifier = MovementModifierController.RunningMovementModifier;
            }


            // If none of the if statements are true, then there is no modifications to movement as default movement will be walking
            Vector3 output = Vector3.zero;

            switch(CoordinateMap) {
                case CoordinateMapType.XToXAndYToY:
                    output = new Vector3(input.x, input.y, UnusedCoordinateValue);
                    break;
                case CoordinateMapType.XToXAndYToZ:
                    output = new Vector3(input.x, UnusedCoordinateValue, input.y);
                    break;
                case CoordinateMapType.XToYAndYToX:
                    output = new Vector3(input.y, input.x, UnusedCoordinateValue);
                    break;
                case CoordinateMapType.XToYAndYToZ:
                    output = new Vector3(UnusedCoordinateValue, input.x, input.y);
                    break;
                case CoordinateMapType.XToZAndYToX:
                    output = new Vector3(input.y, UnusedCoordinateValue, input.x);
                    break;
                case CoordinateMapType.XToZAndYToY:
                    output = new Vector3(UnusedCoordinateValue, input.y, input.x);
                    break;
            }
            output *= movementModifier;

            //if(output.magnitude > 0)
            //    Debug.Log((100*output).ToString() + "/100");
            return output;
        }
    }
}