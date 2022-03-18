package stkengine

import agi.core.AgCoreException
import agi.core.AgCore_JNI

fun main() {
    try {
        AgCore_JNI.xInitThreads()

        StkEngine().compute()
    }
    catch (e: AgCoreException) {
        e.printHexHresult()
        e.printStackTrace()
    }
    catch (t: Throwable) {
        t.printStackTrace()
    }
}