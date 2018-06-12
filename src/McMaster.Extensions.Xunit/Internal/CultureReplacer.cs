// Copyright (c) Nate McMaster.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.Threading;

namespace McMaster.Extensions.Xunit
{
    internal class CultureReplacer : IDisposable
    {
        private readonly CultureInfo _originalCulture;
        private readonly CultureInfo _originalUICulture;
        private readonly long _threadId;

        public CultureReplacer(CultureInfo culture, CultureInfo uiCulture)
        {
            _originalCulture = CultureInfo.CurrentCulture;
            _originalUICulture = CultureInfo.CurrentUICulture;
            _threadId = Thread.CurrentThread.ManagedThreadId;
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = uiCulture;
        }

        /// <summary>
        /// The name of the culture that is used as the default value for CultureInfo.DefaultThreadCurrentCulture when CultureReplacer is used.
        /// </summary>
        public static string DefaultCultureName => DefaultCulture.Name;

        /// <summary>
        /// The name of the culture that is used as the default value for [Thread.CurrentThread(NET45)/CultureInfo(K10)].CurrentUICulture when CultureReplacer is used.
        /// </summary>
        public static string DefaultUICultureName => CultureInfo.InvariantCulture.Name;

        /// <summary>
        /// The culture that is used as the default value for [Thread.CurrentThread(NET45)/CultureInfo(K10)].CurrentCulture when CultureReplacer is used.
        /// </summary>
        public static CultureInfo DefaultCulture => CultureInfo.InvariantCulture;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Thread.CurrentThread.ManagedThreadId != _threadId)
                {
                    throw new InvalidOperationException("The current thread is not the same as the thread invoking the constructor. This should never happen.");
                }

                CultureInfo.CurrentCulture = _originalCulture;
                CultureInfo.CurrentUICulture = _originalUICulture;
            }
        }
    }
}
