# LifeTimeAnalysis
 
## RunLifetimeAnalysis.ipynb

Run trade studies with the Lifetime Tool. Grid searches, Latin Hyper Cube sampling and Monte Carlo analyses are possible. HPOP can also be run to compare to the Lifetime Tool. Multiple instances of STK can be started to speed up the trade studies. Results are saved to a csv file and trade study configurations are saved so they can be loaded and modified later.

## Common Lifetime Questions.ipynb

   * How do density models and solar flux affect lifetime predictions?
   * What are the effects of Gaussian quadrature on lifetime?
   * What are the effects of orbit epoch on lifetime for LEOs?
   * How does the Lifetime Tool compare to HPOP?

   **Summary of Findings**
   * Recommended Lifetime Tool Settings:
       * Turn 2nd Order Oblateness Off
       * Density models
           * Generally use: Jacchia 1970,Jacchia 1971,Jacchia-Roberts,MSIS 1986, MSISE 1990,NRLMSISE 2000s,DTM 2012
           * Generally avoid: 1976 Standard, Harris-Preister, CIRA 1971
       * The SpaceWeather-All-v1.2.txt and the SolFlx_CSSI.dat give similar results for most LEOs. But the SpaceWeather-All-v1.2.txt predicts a higher solar maximum. The SpaceWeather-All-v1.2.txt is also good for historical data.
       * Use the SolFlx_CSSI.dat file for orbit predictions longer than 25 years.
       * Set Gaussian Quadrature to 2. For eccentric orbits maybe bump this up to 4 or 8
       * Set Orbits Per Calculation to 1 for slightly increased accuracy, leave it at the default 10 for better computation speed.
   * Comparing the Lifetime Tool to HPOP for a set of 500+ LEOs, resulted in a mean percent error of 6.6% and a median error of 4.2%. The error is typically lower when the LEOs decayed during solar maximums and the error is typically increased when the LEOs decayed during solar minimums.

## LifetimeOfGTOs.ipynb

**Summary of Findings**

* The Sun and Moon perturbations can have large effects on the orbital lifetime, particularly for certain orbit orientations.
* Due to the highly sensative nature of highly elliptical orbits the orbital lifetime variability is rather large, due to drag, and solar/lunar perturbations.
* The Lifetime Tool offers a good initial guess for GTOs but running HPOP shows a wider variability in outcomes.

## Unexpectedly Short Lifetimes.ipynb

**Summary of Findings**

* Orbits within about +-14 degrees of the critical inclinations (63.4 and 116.6 deg) may fall into orbital resonance with the Sun and Moon which can cause a large growth in eccentricity while keeping the orbital energy fairly constant, eventually causing the satellite to impact Earth. This can occur on the timespan of a few decades. The effect actually seems to become more prominent with larger orbits, in fact more than 50% of orbits near a critical inclination starting at GEO radius decayed in less than 100 years!
* This type of behavior was confirmed by running a few test cases with HPOP and different LifeTime settings (although some satellites would reach a local minimum in the radius of periapsis at the predicted orbital lifetime instead of actually hitting the Earth and would decay a few decades later). Perhaps this inclination could be used for a decay orbit for satellites near the critical inclination such as Molniya orbits or Tundra orbits, although there will be a concern for conjuctions as it nears reentry.
