using System;

namespace StealthSharp.Network
{
    public class BatchRules<TResponse>
    {
        /// <summary>
        /// Creating rule of <see cref="IBatch{TResponse}"/>
        /// </summary>
        public Func<TResponse, IBatch<TResponse>> Create { get; set; }

        /// <summary>
        /// Update rule of <see cref="IBatch{TResponse}"/> 
        /// </summary>
        public Func<IBatch<TResponse>, TResponse, IBatch<TResponse>> Update { get; set; }

        /// <summary>
        /// Default rules for Create and Update
        /// </summary>
        public static BatchRules<TResponse> Default => new BatchRules<TResponse>
        {
            Create = response =>
            {
                var batch = new DefaultBatch<TResponse> {response};
                return batch;
            },
            Update = (batch, response) =>
            {
                batch.Add(response);
                return batch;
            }
        };
    }
}